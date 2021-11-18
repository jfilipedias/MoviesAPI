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

        [HttpPost(Name = "CreateTheater")]
        public IActionResult AddTheater([FromBody] CreateTheaterDto theaterDto)
        {
            var theater = _mapper.Map<Theater>(theaterDto);

            _context.Theaters.Add(theater);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTheaterById), new { Id = theater.Id }, theater);
        }

        [HttpGet(Name = "GetTheaters")]
        public IActionResult GetTheaters()
        {
            return Ok(_context.Theaters);
        }

        [HttpGet("{id}", Name = "GetTheaterByID")]
        public IActionResult GetTheaterById(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null)
                return NotFound();

            var readTheaterDto = _mapper.Map<ReadTheaterDto>(theater);
            return Ok(readTheaterDto);
        }

        [HttpPut("{id}", Name = "UpdateTheater")]
        public IActionResult UpdateTheater(int id, [FromBody] UpdateTheaterDto theaterDto)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null)
                return NotFound();

            _mapper.Map(theaterDto, theater);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTheater")]
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
