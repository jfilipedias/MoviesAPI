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
    [SwaggerTag("Create, read, update and delete adressess")]
    public class AddressController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public AddressController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateAddress")]
        [SwaggerOperation(Summary = "Creates a new address", Description = "Adds a new address to the database")]
        [SwaggerResponse(201, "The address was created", typeof(Address))]
        [SwaggerResponse(400, "The address data is invalid")]
        public IActionResult CreateAddress([FromBody] CreateAddressDto createAddressDto)
        {
            var address = _mapper.Map<Address>(createAddressDto);

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAddressById), new { Id = address.Id }, address);
        }

        [HttpGet(Name = "GetAllAddresses")]
        [SwaggerOperation(Summary = "Lists all addresses", Description = "Return all the addresses in the database")]
        [SwaggerResponse(200, "All existing addresses have been listed", typeof(List<Address>))]
        public IActionResult GetAllAddresses()
        {
            return Ok(_context.Addresses);
        }

        [HttpGet("{id}", Name = "GetAddressById")]
        [SwaggerOperation(Summary = "Lists an address by id", Description = "Lists an address by id")]
        [SwaggerResponse(200, "The given address has been listed", typeof(ReadAddressDto))]
        [SwaggerResponse(404, "The given address was not found")]
        public IActionResult GetAddressById(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return NotFound();

            var readAddressDto = _mapper.Map<ReadAddressDto>(address);
            return Ok(readAddressDto);
        }

        [HttpPut("{id}", Name = "UpdateAddress")]
        [SwaggerOperation(Summary = "Updates an address by id", Description = "Updates an address by id")]
        [SwaggerResponse(204, "The given address has been updated")]
        [SwaggerResponse(404, "The given address was not found")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto updateAddressDto)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return NotFound();

            _mapper.Map(updateAddressDto, address);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteAddress")]
        [SwaggerOperation(Summary = "Deletes an address by id", Description = "Deletes an address by id")]
        [SwaggerResponse(204, "The given address has been deleted")]
        [SwaggerResponse(404, "The given address was not found")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null) return NotFound();

            _context.Remove(address);
            _context.SaveChanges();

            return Ok();
        }
    }
}
