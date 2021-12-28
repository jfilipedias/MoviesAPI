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
        private RoleManager<IdentityRole<int>> _roleManager;
        private UserManager<IdentityUser<int>> _userManager;

        public RegisterService(IEmailProvider emailProvider, IMapper mapper, RoleManager<IdentityRole<int>> roleManager, UserManager<IdentityUser<int>> userManager)
        {
            _emailProvider = emailProvider;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Result RegisterUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            var identityUser = _mapper.Map<IdentityUser<int>>(user);
            var userResult = _userManager.CreateAsync(identityUser, createUserDto.Password).Result;
            var roleResult = _roleManager.CreateAsync(new IdentityRole<int>("admin")).Result;
            var userRoleResult = _userManager.AddToRoleAsync(identityUser, "admin").Result;

            if (!userResult.Succeeded)
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
