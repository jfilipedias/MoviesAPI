using AutoMapper;
using FluentResults;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class TheaterService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public TheaterService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadTheaterDto CreateTheater(CreateTheaterDto createTheaterDto)
        {
            var theater = _mapper.Map<Theater>(createTheaterDto);
            _context.Theaters.Add(theater);
            _context.SaveChanges();

            return _mapper.Map<ReadTheaterDto>(theater);
        }

        public List<ReadTheaterDto>? GetTheaters(string movieName)
        {
            var theaters = _context.Theaters.ToList();

            if (theaters == null) return null;

            if (!String.IsNullOrEmpty(movieName))
            {
                var query = from theater in theaters
                            where theater.Sessions.Any(session =>
                            session.Movie.Title == movieName)
                            select theater;

                theaters = query.ToList();
            }

            return _mapper.Map<List<ReadTheaterDto>>(theaters);
        }

        public ReadTheaterDto? GetTheaterById(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return null;

            return _mapper.Map<ReadTheaterDto>(theater);
        }

        public Result UpdateTheater(int id, UpdateTheaterDto updateTheaterDto)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return Result.Fail("Theater not found.");

            _mapper.Map(updateTheaterDto, theater);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeleteTheater(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return Result.Fail("Theater not found.");

            _context.Remove(theater);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
