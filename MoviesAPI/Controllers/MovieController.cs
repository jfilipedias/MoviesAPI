using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Create, read, update and delete movies")]
    public class MovieController : ControllerBase
    {
        private MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost(Name = "CreateMovie")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Creates a new movie.", Description = "Creates a new movie.")]
        [SwaggerResponse(201, "The movie was created.", typeof(ReadMovieDto))]
        [SwaggerResponse(400, "The movie data is invalid.")]
        public IActionResult CreateMovie([FromBody] CreateMovieDto createMovieDTO)
        {
            var readMovieDto = _movieService.CreateMovie(createMovieDTO);

            return CreatedAtAction(nameof(GetMovieById), new { Id = readMovieDto.Id }, readMovieDto);
        }

        [HttpGet(Name = "GetMovies")]
        [Authorize(Roles = "admin, regular", Policy = "MinimumAge")]
        [SwaggerOperation(Summary = "Gets a movies list.", Description = "Gets a movies list filtered by the given parameters.")]
        [SwaggerResponse(200, "The filtered movies have been listed.", typeof(List<ReadMovieDto>))]
        [SwaggerResponse(404, "Was not found a movie with the given parameters.")]
        public IActionResult GetMovies([FromQuery] int? ageRating = null)
        {
            var readMovieDtos = _movieService.GetMovies(ageRating);
            
            if (readMovieDtos == null) return NotFound();

            return Ok(readMovieDtos);
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        [SwaggerOperation(Summary = "Gets a movie by id.", Description = "Gets a movie by id.")]
        [SwaggerResponse(200, "The movie has been listed.", typeof(ReadMovieDto))]
        [SwaggerResponse(404, "The movie was not found.")]
        public IActionResult GetMovieById(int id)
        {
            var readMovieDto = _movieService.GetMovieById(id);

            if (readMovieDto == null) return NotFound();

            return Ok(readMovieDto );
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        [SwaggerOperation(Summary = "Updates a movie by id.", Description = "Updates a movie by id.")]
        [SwaggerResponse(204, "The given movie has been updated.")]
        [SwaggerResponse(404, "The given movie was not found.")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto updateMovieDTO) 
        {
            var result = _movieService.UpdateMovie(id, updateMovieDTO);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        [SwaggerOperation(Summary = "Deletes a movie by id.", Description = "Deletes a movie by id.")]
        [SwaggerResponse(204, "The given movie has been deleted.")]
        [SwaggerResponse(404, "The given movie was not found.")]
        public IActionResult DeleteMovie(int id)
        {
            var result = _movieService.DeleteMovie(id);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
