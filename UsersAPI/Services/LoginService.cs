using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsersAPI.Data.Requests;

namespace UsersAPI.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private IMapper _mapper;

        public LoginService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result LoginUser(LoginRequest request)
        {
            var identityResult = _signInManager
                .PasswordSignInAsync(request.UserName, request.Password, false, false);

            if (identityResult.Result.Succeeded)
                return Result.Ok();

            return Result.Fail("User could not be authenticated.");
        }
    }
}
