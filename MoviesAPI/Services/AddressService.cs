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

        /// <summary>
        /// Creates a new address in the database.
        /// </summary>
        /// <param name="createAddressDto">Address to be created.</param>
        /// <returns>ReadAddressDto from the created address.</returns>
        public ReadAddressDto CreateAddress(CreateAddressDto createAddressDto)
        {
            var address = _mapper.Map<Address>(createAddressDto);

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return _mapper.Map<ReadAddressDto>(address);
        }

        /// <summary>
        /// Gets a list of address.
        /// </summary>
        /// <returns>ReadAddressDto list.</returns>
        public List<ReadAddressDto>? GetAddresses()
        {
            var addresses = _context.Addresses.ToList();

            return _mapper.Map<List<ReadAddressDto>>(addresses);
        }

        /// <summary>
        /// Gets an address by id.
        /// </summary>
        /// <param name="id">The address id.</param>
        /// <returns>ReadAddressDto from the address.</returns>
        public ReadAddressDto? GetAddressById(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return null;

            return _mapper.Map<ReadAddressDto>(address);
        }

        /// <summary>
        /// Updates an address by id.
        /// </summary>
        /// <param name="id">The address id.</param>
        /// <param name="updateAddressDto">Address info to update.</param>
        /// <returns>Operation result.</returns>
        public Result UpdateAddress(int id, UpdateAddressDto updateAddressDto)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return Result.Fail("Address not found.");

            _mapper.Map(updateAddressDto, address);
            _context.SaveChanges();

            return Result.Ok();
        }

        /// <summary>
        /// Deletes an address by id.
        /// </summary>
        /// <param name="id">The address id.</param>
        /// <returns>Operation result.</returns>
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
