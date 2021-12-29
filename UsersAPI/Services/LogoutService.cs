using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsersAPI.Models;

namespace UsersAPI.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<CustomIdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result LogoutUser()
        {
            var resultIdentity = _signInManager.SignOutAsync();

            if (resultIdentity.IsCompletedSuccessfully)
                return Result.Ok();

            return Result.Fail("User could not be logouted.");
        }
    }
}
