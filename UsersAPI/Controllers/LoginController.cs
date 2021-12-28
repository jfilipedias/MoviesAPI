using FluentResults;
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
        [SwaggerResponse(200, "The given user has been uthenticated.", typeof(List<ISuccess>))]
        [SwaggerResponse(401, "Unauthorized user.", typeof(List<IError>))]
        public IActionResult LoginUser(LoginRequest loginRequest)
        {
            var result = _loginService.LoginUser(loginRequest);

            if (result.IsFailed) 
                return Unauthorized(result.Errors);

            return Ok(result.Successes);
        }

        [HttpPost("/require-reset-password", Name = "Require reset password")]
        [SwaggerOperation(Summary = "Require user password reset.", Description = "Require user password reset.")]
        [SwaggerResponse(200, "The reset password has be required.", typeof(List<ISuccess>))]
        [SwaggerResponse(401, "Unauthorized user.", typeof(List<IError>))]
        public IActionResult RequireResetPassword(RequireResetPasswordRequest resetPasswordRequest)
        {
            var result = _loginService.RequireResetPassword(resetPasswordRequest);

            if (result.IsFailed)
                return Unauthorized(result.Errors);

            return Ok(result.Successes);
        }

        [HttpPost("/reset-password", Name = "Reset password")]
        [SwaggerOperation(Summary = "Reset the user password.", Description = "Reset the user password.")]
        [SwaggerResponse(200, "The password was successfully reseted.", typeof(List<ISuccess>))]
        [SwaggerResponse(401, "The given user was not authorized.", typeof(List<IError>))]
        [SwaggerResponse(500, "The  cannot be reseted.", typeof(List<IError>))]
        public IActionResult ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var result = _loginService.ResetPassword(resetPasswordRequest);

            if (!result.IsFailed)
                return Ok(result.Successes);
            else if (result.HasError(error => error.HasMetadataKey("Unauthorized")))
                return Unauthorized(result.Errors);
            else
                return StatusCode(500, result.Errors);

        }
    }
}
