using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Create, read, update and delete movies")]
    public class MovieController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public MovieController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateMovie")]
        [SwaggerOperation(Summary = "Creates a new movie", Description = "Adds a new movie to the database")]
        [SwaggerResponse(201, "The movie was created", typeof(Movie))]
        [SwaggerResponse(400, "The movie data is invalid")]
        public IActionResult CreateMovie([FromBody] CreateMovieDto createMovieDTO)
        {
            var movie = _mapper.Map<Movie>(createMovieDTO);

            _context.Movies.Add(movie);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
        }

        [HttpGet(Name = "GetMovies")]
        [SwaggerOperation(Summary = "Lists movies", Description = "Return the movies in the database. The movies can be filtered by age rating")]
        [SwaggerResponse(200, "All existing movies have been listed", typeof(List<Movie>))]
        [SwaggerResponse(404, "Was not found a movie with the given filter")]
        public IActionResult GetMovies([FromQuery] int? AgeRating = null)
        {
            List<Movie> movies;

            if (AgeRating == null)
                movies = _context.Movies.ToList();
            else
                movies = _context.Movies.Where(movie => movie.AgeRating <= AgeRating).ToList();

            if (movies == null) return NotFound();

            var readMovieDtos = _mapper.Map<List<ReadMovieDto>>(movies);
            return Ok(readMovieDtos);
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        [SwaggerOperation(Summary = "Lists a movie by id", Description = "Lists a movie by id")]
        [SwaggerResponse(200, "The given movie has been listed", typeof(ReadMovieDto))]
        [SwaggerResponse(404, "The given movie was not found")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            
            if (movie == null) return NotFound();

            var readMovieDTO = _mapper.Map<ReadMovieDto>(movie);
            return Ok(readMovieDTO);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        [SwaggerOperation(Summary = "Updates a movie by id", Description = "Updates a movie by id")]
        [SwaggerResponse(204, "The given movie has been updated")]
        [SwaggerResponse(404, "The given movie was not found")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto updateMovieDTO) 
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return NotFound();

            _mapper.Map(updateMovieDTO, movie);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        [SwaggerOperation(Summary = "Deletes a movie by id", Description = "Deletes a movie by id")]
        [SwaggerResponse(204, "The given movie has been deleted")]
        [SwaggerResponse(404, "The given movie was not found")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return NotFound();

            _context.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
