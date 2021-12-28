using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Web;
using UsersAPI.Data.Dtos;
using UsersAPI.Data.Requests;
using UsersAPI.Models;
using UsersAPI.Providers;

namespace UsersAPI.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private IEmailProvider _emailProvider;
        private UserManager<IdentityUser<int>> _userManager;

        public RegisterService(IMapper mapper, IEmailProvider emailProvider, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _emailProvider = emailProvider;
            _userManager = userManager;
        }

        public Result CreateUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            var identityUser = _mapper.Map<IdentityUser<int>>(user);
            var identityResult = _userManager.CreateAsync(identityUser, createUserDto.Password);

            if (!identityResult.Result.Succeeded)
                return Result.Fail("The user could not be created.");

            var emailConfirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(identityUser).Result;
            var encodedEmailConfirmationToken = HttpUtility.UrlEncode(emailConfirmationToken);
            var message = new Message(new[] { user }, "Account activation code", identityUser.Id, encodedEmailConfirmationToken);

            _emailProvider.SendEmail(message);
            
            return Result.Ok();
        }

        public Result ActivateUserAccount(ActivatesAccountRequest activatesAccountRequest)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(user => user.Id == activatesAccountRequest.UserId);
            var identityResult = _userManager.ConfirmEmailAsync(identityUser, activatesAccountRequest.ActivationCode).Result;

            if (identityResult.Succeeded)
                return Result.Ok();

            return Result.Fail("User account could not be activated.");
        }
    }
}
