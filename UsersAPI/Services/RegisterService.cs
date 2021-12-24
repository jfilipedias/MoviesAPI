﻿using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsersAPI.Data.Dtos;
using UsersAPI.Models;

namespace UsersAPI.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result CreateUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            var identityUser = _mapper.Map<IdentityUser<int>>(user);
            var identityResult = _userManager.CreateAsync(identityUser, createUserDto.Password);

            if (!identityResult.Result.Succeeded)
                return Result.Fail("The user could not be created.");

            var code = _userManager.GenerateEmailConfirmationTokenAsync(identityUser).Result;
            return Result.Ok().WithSuccess(code);
        }
    }
}
