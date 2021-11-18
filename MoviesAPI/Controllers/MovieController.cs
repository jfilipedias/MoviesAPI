using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs.Movie;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "PostMovie")]
        public IActionResult AddMovie([FromBody] CreateMovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Title = movieDTO.Title,
                Director = movieDTO.Director,
                Genre = movieDTO.Genre,
                Duration = movieDTO.Duration
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult GetAllMovies()
        {
            return Ok(_context.Movies);
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            
            if (movie == null)
                return NotFound();

            var movieDTO = new ReadMovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                Duration = movie.Duration,
            };
                
            return Ok(movieDTO);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO movieDto) 
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null)
                return NotFound();

            movie.Title = movieDto.Title;
            movie.Director = movieDto.Director;
            movie.Genre = movieDto.Genre;
            movie.Duration = movieDto.Duration;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete(Name = "DeleteMovie")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null)
                return NotFound();

            _context.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
