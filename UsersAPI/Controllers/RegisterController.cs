using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UsersAPI.Data.Dtos;
using UsersAPI.Services;

namespace UsersAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Create user")]
    public class RegisterController : ControllerBase
    {
        private RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost(Name = "RegisterUser")]
        [SwaggerOperation(Summary = "Creates a new user.", Description = "Creates a new user.")]
        [SwaggerResponse(201, "The user was created.", typeof(List<FluentResults.ISuccess>))]
        [SwaggerResponse(500, "The user could not be created.")]
        public IActionResult RegisterUser(CreateUserDto createUserDto)
        {
            var result = _registerService.CreateUser(createUserDto);

            if (result.IsFailed)
                return StatusCode(500);

            return Ok(result.Successes);
        }
    }
}
