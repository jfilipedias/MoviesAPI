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
        public IActionResult LoginUser(LoginRequest request)
        {
            var result = _loginService.LoginUser(request);

            if (result.IsFailed) 
                return Unauthorized();

            return Ok();
        }
    }
}
