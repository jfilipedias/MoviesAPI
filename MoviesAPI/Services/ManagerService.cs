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

        public ReadManagerDto CreateManager(CreateManagerDto createManagerDto)
        {
            var manager = _mapper.Map<Manager>(createManagerDto);

            _context.Managers.Add(manager);
            _context.SaveChanges();

            return _mapper.Map<ReadManagerDto>(manager);
        }

        public List<ReadManagerDto>? GetManagers()
        {
            var managers = _context.Managers.ToList();

            return _mapper.Map<List<ReadManagerDto>>(managers);
        }

        public ReadManagerDto? GetManagerById(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return null;

            return _mapper.Map<ReadManagerDto>(manager);
        }

        public Result UpdateManager(int id, UpdateManagerDto updateManagerDto)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null) return Result.Fail("Manager not found.");

            _mapper.Map(updateManagerDto, manager);
            _context.SaveChanges();

            return Result.Ok();
        }

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
