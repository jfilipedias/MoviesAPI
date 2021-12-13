using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost(Name = "RegisterUser")]
        public IActionResult RegisterUser(CreateUserDto createUserDto)
        {
            // TODO: Calls the service

            return Ok();
        }
    }
}
