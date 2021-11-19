﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos.Address;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public AddressController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] CreateAddressDto createAddressDto)
        {
            var address = _mapper.Map<Address>(createAddressDto);

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAddressById), new { Id = address.Id }, address);
        }

        [HttpGet]
        public IActionResult GetAddress()
        {
            return Ok(_context.Addresses);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null)
                return NotFound();

            var readAddressDto = _mapper.Map<ReadAddressDto>(address);
            return Ok(readAddressDto);
        }

        [HttpGet("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto updateAddressDto)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null)
                return NotFound();

            _mapper.Map(updateAddressDto, address);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null)
                return NotFound();

            _context.Remove(address);
            _context.SaveChanges();

            return Ok();
        }
    }
}
