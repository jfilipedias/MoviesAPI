using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Create, read, update and delete managers")]
    public class ManagerController : ControllerBase
    {
        private ManagerService _managerService;
        
        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost(Name = "CreateManager")]
        [SwaggerOperation(Summary = "Creates a new manager.", Description = "Creates a new manager.")]
        [SwaggerResponse(201, "The manager was created.", typeof(ReadManagerDto))]
        [SwaggerResponse(400, "The manager data is invalid.")]
        public IActionResult CreateManager([FromBody] CreateManagerDto createManagerDto)
        {
            ReadManagerDto readManagerDto = _managerService.CreateManager(createManagerDto);

            return CreatedAtAction(nameof(GetManagerById), new { Id = readManagerDto.Id }, readManagerDto);
        }

        [HttpGet(Name = "GetManagers")]
        [SwaggerOperation(Summary = "Gets a managers list.", Description = "Gets a managers list.")]
        [SwaggerResponse(200, "All existing managers have been listed.", typeof(List<ReadManagerDto>))]
        public IActionResult GetManagers()
        {
            var readManagerDto = _managerService.GetManagers();
            
            return Ok(readManagerDto);
        }

        [HttpGet("{id}", Name = "GetManagerById")]
        [SwaggerOperation(Summary = "Gets a manager by id.", Description = "Gets a manager by id.")]
        [SwaggerResponse(200, "The given manager has been listed.", typeof(ReadManagerDto))]
        [SwaggerResponse(404, "The given manager was not found.")]
        public IActionResult GetManagerById(int id)
        {
            var readManagerDto = _managerService.GetManagerById(id);

            if (readManagerDto == null) return NotFound();

            return Ok(readManagerDto);
        }

        [HttpPut("{id}", Name = "UpdateManager")]
        [SwaggerOperation(Summary = "Updates a manager by id.", Description = "Updates a manager by id.")]
        [SwaggerResponse(204, "The given manager has been updated.")]
        [SwaggerResponse(404, "The given manager was not found.")]
        public IActionResult UpdateManger(int id, [FromBody] UpdateManagerDto updateManagerDto)
        {
            var result = _managerService.UpdateManager(id, updateManagerDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteManager")]
        [SwaggerOperation(Summary = "Deletes a manager by id.", Description = "Deletes a manager by id.")]
        [SwaggerResponse(204, "The given manager has been deleted.")]
        [SwaggerResponse(404, "The given manager was not found.")]
        public IActionResult DeleteManager(int id)
        {
            var result = _managerService.DeleteManager(id);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
