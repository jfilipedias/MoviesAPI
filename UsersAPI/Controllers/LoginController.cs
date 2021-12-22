using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UsersAPI.Data.Requests;
using UsersAPI.Services;

namespace UsersAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Authenticate user")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost(Name = "Login user")]
        [SwaggerOperation(Summary = "Login an user and return a JWT.", Description = "Login an user and return a JWT.")]
        [SwaggerResponse(200, "The given user has been uthenticated.", typeof(List<FluentResults.ISuccess>))]
        [SwaggerResponse(401, "The given user was not authorized.")]
        public IActionResult LoginUser(LoginRequest request)
        {
            var result = _loginService.LoginUser(request);

            if (result.IsFailed) 
                return Unauthorized(result.Errors);

            return Ok(result.Successes);
        }
    }
}
