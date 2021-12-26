using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Web;
using UsersAPI.Data.Dtos;
using UsersAPI.Data.Requests;
using UsersAPI.Models;

namespace UsersAPI.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
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

            _emailService.SendEmail(new [] { user }, "Account activation link", identityUser.Id, encodedEmailConfirmationToken);
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
