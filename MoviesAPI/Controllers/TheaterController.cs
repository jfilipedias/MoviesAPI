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
    [SwaggerTag("Create, read, update and delete managers")]
    public class TheaterController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public TheaterController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateTheater")]
        [SwaggerOperation(Summary = "Creates a new theater", Description = "Adds a new theater to the database")]
        [SwaggerResponse(201, "The theater was created", typeof(Manager))]
        [SwaggerResponse(400, "The theater data is invalid")]
        public IActionResult CreateTheater([FromBody] CreateTheaterDto createTheaterDto)
        {
            var theater = _mapper.Map<Theater>(createTheaterDto);

            _context.Theaters.Add(theater);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTheaterById), new { Id = theater.Id }, theater);
        }

        [HttpGet(Name = "GetAllTheaters")]
        [SwaggerOperation(Summary = "Lists all theaters", Description = "Return all the theaters in the database")]
        [SwaggerResponse(200, "All existing theaters have been listed", typeof(List<Manager>))]
        public IActionResult GetAllTheaters()
        {
            return Ok(_context.Theaters);
        }

        [HttpGet("{id}", Name = "GetTheaterById")]
        [SwaggerOperation(Summary = "Lists a theater by id", Description = "Lists a theater by id")]
        [SwaggerResponse(200, "The given theater has been listed", typeof(ReadManagerDto))]
        [SwaggerResponse(404, "The given theater was not found")]
        public IActionResult GetTheaterById(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return NotFound();

            var readTheaterDto = _mapper.Map<ReadTheaterDto>(theater);
            return Ok(readTheaterDto);
        }

        [HttpPut("{id}", Name = "UpdateTheater")]
        [SwaggerOperation(Summary = "Updates a theater by id", Description = "Updates a theater by id")]
        [SwaggerResponse(204, "The given theater has been updated")]
        [SwaggerResponse(404, "The given theater was not found")]
        public IActionResult UpdateTheater(int id, [FromBody] UpdateTheaterDto updateTheaterDto)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return NotFound();

            _mapper.Map(updateTheaterDto, theater);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTheater")]
        [SwaggerOperation(Summary = "Deletes a theater by id", Description = "Deletes a theater by id")]
        [SwaggerResponse(204, "The given theater has been deleted")]
        [SwaggerResponse(404, "The given theater was not found")]
        public IActionResult DeleteTheater(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return NotFound();

            _context.Remove(theater);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
