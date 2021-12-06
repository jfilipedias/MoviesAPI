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
    [SwaggerTag("Create, read, update and delete sessions")]
    public class SessionController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SessionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateSession")]
        [SwaggerOperation(Summary = "Creates a new session", Description = "Adds a new sessionto the database")]
        [SwaggerResponse(201, "The session was created", typeof(Session))]
        [SwaggerResponse(400, "The session data is invalid")]
        public IActionResult CreateSession([FromBody] CreateSessionDto createSessionDto)
        {
            var session = _mapper.Map<Session>(createSessionDto);

            _context.Sessions.Add(session);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSessionById), new { Id = session.Id }, session);
        }

        [HttpGet(Name = "GetAllSessions")]
        [SwaggerOperation(Summary = "Lists all sessions", Description = "Return the sessions in the database")]
        [SwaggerResponse(200, "All existing sessions have been listed", typeof(List<Session>))]
        public IActionResult GetAllSessions()
        {
            return Ok(_context.Sessions);
        }

        [HttpGet("{id}", Name = "GetSessionById")]
        [SwaggerOperation(Summary = "Lists a session by id", Description = "Lists a session by id")]
        [SwaggerResponse(200, "The given session has been listed", typeof(ReadSessionDto))]
        [SwaggerResponse(400, "The given session was not found")]
        public IActionResult GetSessionById(int id)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session == null) return NotFound();

            var readTheaterDto = _mapper.Map<ReadSessionDto>(session);
            return Ok(readTheaterDto);
        }

        [HttpPut("{id}", Name = "UpdateSession")]
        [SwaggerOperation(Summary = "Updates a session by id", Description = "Updates a session by id")]
        [SwaggerResponse(204, "The given session has been updated")]
        [SwaggerResponse(404, "The given session was not found")]
        public IActionResult UpdateSession(int id, [FromBody] UpdateSessionDto updateSessionDto)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session == null) return NotFound();

            _mapper.Map(updateSessionDto, session);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteSessions")]
        [SwaggerOperation(Summary = "Deletes a session by id", Description = "Deletes a session by id")]
        [SwaggerResponse(204, "The given session has been deleted")]
        [SwaggerResponse(404, "The given session was not found")]
        public IActionResult DeleteSession(int id)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session == null) return NotFound();

            _context.Remove(session);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
