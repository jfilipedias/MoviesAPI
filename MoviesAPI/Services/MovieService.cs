using AutoMapper;
using FluentResults;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class MovieService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public MovieService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadMovieDto CreateMovie(CreateMovieDto createMovieDTO)
        {
            var movie = _mapper.Map<Movie>(createMovieDTO);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            
            return _mapper.Map<ReadMovieDto>(movie);
        }

        public List<ReadMovieDto>? GetMovies(int? ageRating)
        {
            var movies = _context.Movies.ToList();

            if (movies == null) return null;

            if (ageRating != null)
                movies = movies.Where(movie => movie.AgeRating <= ageRating).ToList();

            return _mapper.Map<List<ReadMovieDto>>(movies);
        }

        public ReadMovieDto? GetMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return null;

            return _mapper.Map<ReadMovieDto>(movie);
        }

        public Result UpdateMovie(int id, UpdateMovieDto updateMovieDTO)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return Result.Fail("Movie not found.");

            _mapper.Map(updateMovieDTO, movie);
            _context.SaveChanges();

            return  Result.Ok();
        }

        public Result DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return Result.Fail("Movie not found.");

            _context.Remove(movie);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
