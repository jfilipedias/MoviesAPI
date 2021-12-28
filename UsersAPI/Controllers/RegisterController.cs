using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UsersAPI.Data.Dtos;
using UsersAPI.Data.Requests;
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
        [SwaggerResponse(201, "The user was created.", typeof(List<ISuccess>))]
        [SwaggerResponse(500, "The user could not be created.", typeof(List<IError>))]
        public IActionResult RegisterUser(CreateUserDto createUserDto)
        {
            var result = _registerService.RegisterUser(createUserDto);

            if (result.IsFailed)
                return StatusCode(500, result.Errors);

            return Ok(result.Successes);
        }
        
        [HttpGet("/activates", Name = "ActivateUserAccount")]
        [SwaggerResponse(201, "The user account was activated.", typeof(List<ISuccess>))]
        [SwaggerResponse(500, "The user account could not be activated.", typeof(List<IError>))]
        public IActionResult ActivateUserAccount([FromQuery]ActivatesAccountRequest activatesAccountRequest)
        {
            var result = _registerService.ActivateUserAccount(activatesAccountRequest);

            if (result.IsFailed)
                return StatusCode(500, result.Errors);

            return Ok(result.Successes);
        }
    }
}
