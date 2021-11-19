using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos.Theater;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public TheaterController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateTheater([FromBody] CreateTheaterDto createTheaterDto)
        {
            var theater = _mapper.Map<Theater>(createTheaterDto);

            _context.Theaters.Add(theater);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTheaterById), new { Id = theater.Id }, theater);
        }

        [HttpGet]
        public IActionResult GetAllTheaters()
        {
            return Ok(_context.Theaters);
        }

        [HttpGet("{id}")]
        public IActionResult GetTheaterById(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null)
                return NotFound();

            var readTheaterDto = _mapper.Map<ReadTheaterDto>(theater);
            return Ok(readTheaterDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTheater(int id, [FromBody] UpdateTheaterDto updateTheaterDto)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null)
                return NotFound();

            _mapper.Map(updateTheaterDto, theater);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTheater(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null)
                return NotFound();

            _context.Remove(theater);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
