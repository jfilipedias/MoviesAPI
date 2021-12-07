using AutoMapper;
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

        /// <summary>
        /// Creates a new movie in the database.
        /// </summary>
        /// <param name="createMovieDTO">Movie to be created.</param>
        /// <returns>ReadMovieDto from the created movie.</returns>
        public ReadMovieDto? CreateMovie(CreateMovieDto createMovieDTO)
        {
            var movie = _mapper.Map<Movie>(createMovieDTO);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            
            return _mapper.Map<ReadMovieDto>(movie);
        }

        /// <summary>
        /// Gets a list of movies filtered by the given parameters.
        /// </summary>
        /// <param name="ageRating">Age rating to be filtered.</param>
        /// <returns>ReadMovieDto list from the filtered movies.</returns>
        public List<ReadMovieDto>? GetMovies(int? ageRating)
        {
            var movies = _context.Movies.ToList();

            if (movies == null) return null;

            if (ageRating != null)
                movies = movies.Where(movie => movie.AgeRating <= ageRating).ToList();

            return _mapper.Map<List<ReadMovieDto>>(movies);
        }

        /// <summary>
        /// Gets a movie by id.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <returns>ReadMovieDto from the movie.</returns>
        public ReadMovieDto? GetMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return null;

            return _mapper.Map<ReadMovieDto>(movie);
        }

        /// <summary>
        /// Updates a movie by id.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <param name="updateMovieDTO">Movies info to update.</param>
        /// <returns>ReadMovieDto from the updated movie.</returns>
        public ReadMovieDto? UpdateMovie(int id, UpdateMovieDto updateMovieDTO)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return null;

            _mapper.Map(updateMovieDTO, movie);
            _context.SaveChanges();

            var readMovieDto = _mapper.Map<ReadMovieDto>(updateMovieDTO);
            readMovieDto.Id = id;

            return readMovieDto;
        }

        /// <summary>
        /// Deletes a movie by id.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <returns>ReadMovieDto from the deleted movie.</returns>
        public ReadMovieDto? DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return null;

            _context.Remove(movie);
            _context.SaveChanges();

            return _mapper.Map<ReadMovieDto>(movie);
        }
    }
}
