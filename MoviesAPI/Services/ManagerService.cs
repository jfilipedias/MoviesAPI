using AutoMapper;
using FluentResults;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class ManagerService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public ManagerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new manager in the database.
        /// </summary>
        /// <param name="createManagerDto">Manager to be created.</param>
        /// <returns>ReadManagerDto from the created manager.</returns>
        public ReadManagerDto CreateManager(CreateManagerDto createManagerDto)
        {
            var manager = _mapper.Map<Manager>(createManagerDto);

            _context.Managers.Add(manager);
            _context.SaveChanges();

            return _mapper.Map<ReadManagerDto>(manager);
        }

        /// <summary>
        /// Gets a list of managers.
        /// </summary>
        /// <returns>ReadManagerDto list.</returns>
        public List<ReadManagerDto>? GetManagers()
        {
            var managers = _context.Managers.ToList();

            return _mapper.Map<List<ReadManagerDto>>(managers);
        }

        /// <summary>
        /// Gets a manager by id.
        /// </summary>
        /// <param name="id">The manager id.</param>
        /// <returns>ReadManagerDto from the manager.</returns>
        public ReadManagerDto? GetManagerById(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return null;

            return _mapper.Map<ReadManagerDto>(manager);
        }

        /// <summary>
        /// Updates a manager by id.
        /// </summary>
        /// <param name="id">The manager id.</param>
        /// <param name="updateManagerDto">Manager info to update.</param>
        /// <returns>Operation result.</returns>
        public Result UpdateManager(int id, UpdateManagerDto updateManagerDto)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return Result.Fail("Manager not found.");

            _mapper.Map(updateManagerDto, manager);
            _context.SaveChanges();

            return Result.Ok();
        }

        /// <summary>
        /// Deletes an manager by id.
        /// </summary>
        /// <param name="id">The manager id.</param>
        /// <returns>Operation result.</returns>
        public Result DeleteManager(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return Result.Fail("Manager not found.");

            _context.Remove(manager);
            _context.SaveChanges();

            return Result.Ok();

        }
    }
}
