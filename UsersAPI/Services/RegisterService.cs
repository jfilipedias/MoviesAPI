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
        private IEmailProvider _emailProvider;
        private IMapper _mapper;
        private UserManager<CustomIdentityUser<int>> _userManager;

        public RegisterService(IEmailProvider emailProvider, IMapper mapper, UserManager<CustomIdentityUser<int>> userManager)
        {
            _emailProvider = emailProvider;
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result RegisterUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            var customIdentityUser = _mapper.Map<CustomIdentityUser<int>>(user);
            var userResult = _userManager.CreateAsync(customIdentityUser, createUserDto.Password).Result;
            
            _userManager.AddToRoleAsync(customIdentityUser, "regular");

            if (!userResult.Succeeded)
                return Result.Fail("The user could not be created.");

            var emailConfirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(customIdentityUser).Result;
            var encodedEmailConfirmationToken = HttpUtility.UrlEncode(emailConfirmationToken);
            var message = new Message(new[] { user }, "Account activation code", customIdentityUser.Id, encodedEmailConfirmationToken);

            _emailProvider.SendEmail(message);
            
            return Result.Ok();
        }

        public Result ActivateUserAccount(ActivatesAccountRequest activatesAccountRequest)
        {
            var customIdentityUser = _userManager
                .Users
                .FirstOrDefault(user => user.Id == activatesAccountRequest.UserId);

            var identityResult = _userManager.ConfirmEmailAsync(customIdentityUser, activatesAccountRequest.ActivationCode).Result;

            if (identityResult.Succeeded)
                return Result.Ok();

            return Result.Fail("User account could not be activated.");
        }
    }
}
