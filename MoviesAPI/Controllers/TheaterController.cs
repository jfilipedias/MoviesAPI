using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;
using MoviesAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Create, read, update and delete managers")]
    public class TheaterController : ControllerBase
    {
        private TheaterService _theaterService;

        public TheaterController(TheaterService theaterService)
        {
            _theaterService = theaterService;
        }

        [HttpPost(Name = "CreateTheater")]
        [SwaggerOperation(Summary = "Creates a new theater.", Description = "Creates a new theater.")]
        [SwaggerResponse(201, "The theater was created.", typeof(Theater))]
        [SwaggerResponse(400, "The theater data is invalid.")]
        public IActionResult CreateTheater([FromBody] CreateTheaterDto createTheaterDto)
        {
            var readTheaterDto = _theaterService.CreateTheater(createTheaterDto);

            return CreatedAtAction(nameof(GetTheaterById), new { Id = readTheaterDto.Id }, readTheaterDto);
        }

        [HttpGet(Name = "GetTheaters")]
        [SwaggerOperation(Summary = "Gets a theaters list.", Description = "Gets a theaters list filtered by the given parameters.")]
        [SwaggerResponse(200, "The filtered theaters have been listed.", typeof(List<ReadTheaterDto>))]
        [SwaggerResponse(404, "Was not found a theater with the given parameters.")]
        public IActionResult GetTheaters([FromQuery] string movieName)
        {
            var readTheaterDtos = _theaterService.GetTheaters(movieName);
            
            if (readTheaterDtos == null) return NotFound();

            return Ok(readTheaterDtos);
        }

        [HttpGet("{id}", Name = "GetTheaterById")]
        [SwaggerOperation(Summary = "Gets a theater by id.", Description = "Gets a theater by id.")]
        [SwaggerResponse(200, "The theater has been listed.", typeof(ReadTheaterDto))]
        [SwaggerResponse(404, "The theater was not found.")]
        public IActionResult GetTheaterById(int id)
        {
            var readTheaterDto = _theaterService.GetTheaterById(id);

            if (readTheaterDto == null) return NotFound();

            return Ok(readTheaterDto);
        }

        [HttpPut("{id}", Name = "UpdateTheater")]
        [SwaggerOperation(Summary = "Updates a theater by id.", Description = "Updates a theater by id.")]
        [SwaggerResponse(204, "The given theater has been updated.")]
        [SwaggerResponse(404, "The given theater was not found.")]
        public IActionResult UpdateTheater(int id, [FromBody] UpdateTheaterDto updateTheaterDto)
        {
            var readTheaterDto = _theaterService.UpdateTheater(id, updateTheaterDto);
            
            if (readTheaterDto == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTheater")]
        [SwaggerOperation(Summary = "Deletes a theater by id.", Description = "Deletes a theater by id.")]
        [SwaggerResponse(204, "The theater has been deleted.")]
        [SwaggerResponse(404, "The theater was not found.")]
        public IActionResult DeleteTheater(int id)
        {
            var readTheaterDto = _theaterService.DeleteTheater(id);

            if (readTheaterDto == null) return NotFound();

            return NoContent();
        }
    }
}
