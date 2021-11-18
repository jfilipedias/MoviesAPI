using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
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
        public IActionResult AddMovie([FromBody] Movie movie)
        {
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
                
            return Ok(movie);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        public IActionResult UpdateMovie(int id, [FromBody] Movie newMovie) 
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null)
                return NotFound();

            movie.Title = newMovie.Title;
            movie.Director = newMovie.Director;
            movie.Genre = newMovie.Genre;
            movie.Duration = newMovie.Duration;

            _context.SaveChanges();

            return NoContent();
        }
    }
}
