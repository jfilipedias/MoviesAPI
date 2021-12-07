using AutoMapper;
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

        /// <summary>
        /// Creates a new movie in the database.
        /// </summary>
        /// <param name="createTheaterDto">Theater to be created.</param>
        /// <returns>ReadTheaterDto from the created theater.</returns>
        public ReadTheaterDto CreateTheater(CreateTheaterDto createTheaterDto)
        {
            var theater = _mapper.Map<Theater>(createTheaterDto);
            _context.Theaters.Add(theater);
            _context.SaveChanges();

            return _mapper.Map<ReadTheaterDto>(theater);
        }

        /// <summary>
        /// Gets a list of theaters filtered by the given parameters.
        /// </summary>
        /// <param name="movieName">Movie name to be filtered.</param>
        /// <returns>ReadTheaterDto list from the filtered theaters.</returns>
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

        /// <summary>
        /// Gets a theater by id.
        /// </summary>
        /// <param name="id">The theater id.</param>
        /// <returns>ReateTheaterDto from the theater.</returns>
        public ReadTheaterDto? GetTheaterById(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return null;

            return _mapper.Map<ReadTheaterDto>(theater);
        }

        /// <summary>
        /// Updates a theater by id.
        /// </summary>
        /// <param name="id">The theater id.</param>
        /// <param name="updateTheaterDto">Theater info to update.</param>
        /// <returns>ReadTheaterDto from the updated theater.</returns>
        public ReadTheaterDto? UpdateTheater(int id, UpdateTheaterDto updateTheaterDto)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return null;

            _mapper.Map(updateTheaterDto, theater);
            _context.SaveChanges();
            
            var readTheaterDto = _mapper.Map<ReadTheaterDto>(updateTheaterDto);
            readTheaterDto.Id = id;

            return readTheaterDto;
        }

        /// <summary>
        /// Deletes a theater by id.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <returns>ReadTheaterDto from the deleted theater.</returns>
        public ReadTheaterDto? DeleteTheater(int id)
        {
            var theater = _context.Theaters.FirstOrDefault(theater => theater.Id == id);

            if (theater == null) return null;

            _context.Remove(theater);
            _context.SaveChanges();

            return _mapper.Map<ReadTheaterDto>(theater);
        }
    }
}
