using AutoMapper;
using FluentResults;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class SessionService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SessionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadSessionDto CreateSession(CreateSessionDto createSessionDto)
        {
            var session = _mapper.Map<Session>(createSessionDto);

            _context.Sessions.Add(session);
            _context.SaveChanges();

            return _mapper.Map<ReadSessionDto>(session);
        }

        public List<ReadSessionDto>? GetSession()
        {
            var sessions = _context.Sessions.ToList();

            return _mapper.Map<List<ReadSessionDto>>(sessions);
        }

        public ReadSessionDto? GetSessionById(int id)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session == null) return null;

            return _mapper.Map<ReadSessionDto>(session);
        }

        public Result UpdateSession(int id, UpdateSessionDto updateSessionDto)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session == null) return Result.Fail("Session not found.");

            _mapper.Map(updateSessionDto, session);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeleteSession(int id)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session == null) return Result.Fail("Session not found.");

            _context.Remove(session);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
