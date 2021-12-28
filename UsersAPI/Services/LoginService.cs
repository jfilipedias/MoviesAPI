using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsersAPI.Data.Requests;

namespace UsersAPI.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LoginUser(LoginRequest request)
        {
            var identityResult = _signInManager
                .PasswordSignInAsync(request.UserName, request.Password, false, false);

            if (identityResult.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user => user.NormalizedUserName == request.UserName.ToUpper());

                var token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail("Unauthorized user.");
        }

        public Result RequireResetPassword(RequireResetPasswordRequest resetPasswordRequest)
        {
            var identityUser = GetUserByEmail(resetPasswordRequest.Email);

            if (identityUser == null)
                return Result.Fail("Unauthorized user.");

            var passwordResetToken = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
            return Result.Ok().WithSuccess(passwordResetToken);
        }
        }
    }
}
