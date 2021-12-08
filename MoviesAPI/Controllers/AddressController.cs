using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Create, read, update and delete adressess")]
    public class AddressController : ControllerBase
    {
        private AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost(Name = "CreateAddress")]
        [SwaggerOperation(Summary = "Creates a new address.", Description = "Creates a new address.")]
        [SwaggerResponse(201, "The address was created.", typeof(ReadAddressDto))]
        [SwaggerResponse(400, "The address data is invalid.")]
        public IActionResult CreateAddress([FromBody] CreateAddressDto createAddressDto)
        {
            var readAddressDto = _addressService.CreateAddress(createAddressDto);
            
            return CreatedAtAction(nameof(GetAddressById), new { Id = readAddressDto.Id }, readAddressDto);
        }

        [HttpGet(Name = "GetAddresses")]
        [SwaggerOperation(Summary = "Gets all addresses.", Description = "Gets all addresses.")]
        [SwaggerResponse(200, "All existing addresses have been listed.", typeof(List<ReadAddressDto>))]
        public IActionResult GetAddresses()
        {
            var readAddressDto = _addressService.GetAddresses();

            return Ok(readAddressDto);
        }

        [HttpGet("{id}", Name = "GetAddressById")]
        [SwaggerOperation(Summary = "Gets an address by id.", Description = "Gets an address by id.")]
        [SwaggerResponse(200, "The given address has been listed.", typeof(ReadAddressDto))]
        [SwaggerResponse(404, "The given address was not found.")]
        public IActionResult GetAddressById(int id)
        {
            var readAddressDto = _addressService.GetAddressById(id);

            if (readAddressDto == null) return NotFound();

            return Ok(readAddressDto);
        }

        [HttpPut("{id}", Name = "UpdateAddress")]
        [SwaggerOperation(Summary = "Updates an address by id.", Description = "Updates an address by id.")]
        [SwaggerResponse(204, "The given address has been updated.")]
        [SwaggerResponse(404, "The given address was not found.")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto updateAddressDto)
        {
            var result = _addressService.UpdateAddress(id, updateAddressDto);
            
            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteAddress")]
        [SwaggerOperation(Summary = "Deletes an address by id.", Description = "Deletes an address by id.")]
        [SwaggerResponse(204, "The given address has been deleted")]
        [SwaggerResponse(404, "The given address was not found")]
        public IActionResult DeleteAddress(int id)
        {
            var result = _addressService.DeleteAddress(id);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
