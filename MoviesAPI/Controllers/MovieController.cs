using AutoMapper;
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
        private IMapper _mapper;

        public MovieController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost(Name = "PostMovie")]
        public IActionResult AddMovie([FromBody] CreateMovieDto movieDTO)
        {
            var movie = _mapper.Map<Movie>(movieDTO);

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

            var movieDTO = _mapper.Map<ReadMovieDto>(movie);
                
            return Ok(movieDTO);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDTO) 
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null)
                return NotFound();

            _mapper.Map(movieDTO, movie);
            
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
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
