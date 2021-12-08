using AutoMapper;
using FluentResults;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class AddressService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public AddressService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadAddressDto CreateAddress(CreateAddressDto createAddressDto)
        {
            var address = _mapper.Map<Address>(createAddressDto);

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return _mapper.Map<ReadAddressDto>(address);
        }

        public List<ReadAddressDto>? GetAddresses()
        {
            var addresses = _context.Addresses.ToList();

            return _mapper.Map<List<ReadAddressDto>>(addresses);
        }

        public ReadAddressDto? GetAddressById(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return null;

            return _mapper.Map<ReadAddressDto>(address);
        }

        public Result UpdateAddress(int id, UpdateAddressDto updateAddressDto)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return Result.Fail("Address not found.");

            _mapper.Map(updateAddressDto, address);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeleteAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return Result.Fail("Address not found.");

            _context.Remove(address);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
