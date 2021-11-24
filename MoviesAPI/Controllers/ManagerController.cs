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
        [SwaggerOperation(Summary = "Creates a new manager", Description = "Adds a new manager to the database")]
        [SwaggerResponse(201, "The manager was created", typeof(Manager))]
        [SwaggerResponse(400, "The manager data is invalid")]
        public IActionResult CreateManager([FromBody] CreateManagerDto createManagerDto)
        {
            var manager = _mapper.Map<Manager>(createManagerDto);
            _context.Managers.Add(manager);
            _context.SaveChanges();

            return Ok(manager);
        }

        [HttpGet(Name = "GetAllManagers")]
        [SwaggerOperation(Summary = "Lists all managers", Description = "Return all the managers in the database")]
        [SwaggerResponse(200, "All existing managers have been listed", typeof(List<Manager>))]
        public IActionResult GetAllManagers()
        {
            return Ok(_context.Movies);
        }

        [HttpGet("{id}", Name = "GetManagerById")]
        [SwaggerOperation(Summary = "Lists a manager by id", Description = "Lists a manager by id")]
        [SwaggerResponse(200, "The given manager has been listed", typeof(ReadManagerDto))]
        [SwaggerResponse(404, "The given manager was not found")]
        public IActionResult GetManagerById(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return NotFound();

            var readManagerDto = _mapper.Map<ReadManagerDto>(manager);

            return Ok(readManagerDto);
        }

        [HttpPut("{id}", Name = "UpdateManager")]
        [SwaggerOperation(Summary = "Updates a manager by id", Description = "Updates a manager by id")]
        [SwaggerResponse(200, "The given manager has been updated")]
        [SwaggerResponse(404, "The given manager was not found")]
        public IActionResult UpdateManger(int id, [FromBody] UpdateManagerDto updateManagerDto)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return NotFound();

            _mapper.Map(updateManagerDto, manager);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteManager")]
        [SwaggerOperation(Summary = "Deletes a manager by id", Description = "Deletes a manager by id")]
        [SwaggerResponse(200, "The given manager has been deleted")]
        [SwaggerResponse(404, "The given manager was not found")]
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
