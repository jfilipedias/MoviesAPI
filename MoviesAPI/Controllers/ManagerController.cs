using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        
        public ManagerController(AppDbContext context, IMapper _mapper)
        {
            _context = context;
            _mapper = _mapper;
        }

        [HttpPost(Name = "CreateManager")]
        public IActionResult CreateManager([FromBody] CreateManagerDto createManagerDto)
        {
            var manager = _mapper.Map<Manager>(createManagerDto);
            _context.Managers.Add(manager);
            _context.SaveChanges();

            return Ok(manager);
        }

        [HttpGet(Name = "GetAllManagers")]
        public IActionResult GetAllManagers()
        {
            return Ok(_context.Movies);
        }

        [HttpGet("{id}", Name = "GetManagerById")]
        public IActionResult GetManagerById(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return NotFound();

            var readManagerDto = _mapper.Map<ReadManagerDto>(manager);

            return Ok(readManagerDto);
        }

        [HttpPut("{id}", Name = "UpdateManager")]
        public IActionResult UpdateManger(int id, [FromBody] UpdateManagerDto updateManagerDto)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return NotFound();

            _mapper.Map(updateManagerDto, manager);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteManager")]
        public IActionResult DeleteManager(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return NotFound();

            _context.Remove(manager);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
