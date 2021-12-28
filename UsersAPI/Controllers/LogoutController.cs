using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UsersAPI.Services;

namespace UsersAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Finish user session")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost(Name = "Logout user")]
        [SwaggerOperation(Summary = "Finishes the user session.", Description = "Finishes the user session.")]
        [SwaggerResponse(200, "The given user has been logouted.", typeof(List<ISuccess>))]
        public IActionResult LogoutUser()
        {
            var result = _logoutService.LogoutUser();

            if (result.IsFailed)
                return Unauthorized(result.Errors);

            return Ok(result.Successes);
        }
    }
}
