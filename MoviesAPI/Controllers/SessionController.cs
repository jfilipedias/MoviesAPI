using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Create, read, update and delete sessions")]
    public class SessionController : ControllerBase
    {
        private SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost(Name = "CreateSession")]
        [SwaggerOperation(Summary = "Creates a new session.", Description = "Creates a new session.")]
        [SwaggerResponse(201, "The session was created.", typeof(ReadSessionDto))]
        [SwaggerResponse(400, "The session data is invalid.")]
        public IActionResult CreateSession([FromBody] CreateSessionDto createSessionDto)
        {
            var readSessionDto = _sessionService.CreateSession(createSessionDto);
            
            return CreatedAtAction(nameof(GetSessionById), new { Id = readSessionDto.Id }, readSessionDto);
        }

        [HttpGet(Name = "GetSessions")]
        [SwaggerOperation(Summary = "Gets a sessions list.", Description = "Gets a sessions list.")]
        [SwaggerResponse(200, "All existing sessions have been listed.", typeof(List<ReadSessionDto>))]
        public IActionResult GetSessions()
        {
            var readSessionDtos = _sessionService.GetSession();
            
            return Ok(readSessionDtos);
        }

        [HttpGet("{id}", Name = "GetSessionById")]
        [SwaggerOperation(Summary = "Gets a session by id.", Description = "Gets a session by id.")]
        [SwaggerResponse(200, "The given session has been listed.", typeof(ReadSessionDto))]
        [SwaggerResponse(400, "The given session was not found.")]
        public IActionResult GetSessionById(int id)
        {
            ReadSessionDto? readSessionDto = _sessionService.GetSessionById(id);

            if (readSessionDto == null) return NotFound();

            return Ok(readSessionDto);
        }

        [HttpPut("{id}", Name = "UpdateSession")]
        [SwaggerOperation(Summary = "Updates a session by id.", Description = "Updates a session by id.")]
        [SwaggerResponse(204, "The given session has been updated.")]
        [SwaggerResponse(404, "The given session was not found.")]
        public IActionResult UpdateSession(int id, [FromBody] UpdateSessionDto updateSessionDto)
        {
            var result = _sessionService.UpdateSession(id, updateSessionDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteSessions")]
        [SwaggerOperation(Summary = "Deletes a session by id.", Description = "Deletes a session by id.")]
        [SwaggerResponse(204, "The given session has been deleted.")]
        [SwaggerResponse(404, "The given session was not found.")]
        public IActionResult DeleteSession(int id)
        {
            var result = _sessionService.DeleteSession(id);
            
            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
