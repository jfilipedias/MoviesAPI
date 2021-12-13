﻿using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Data.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The passwords does not match.")]
        public string Repassword { get; set; }
    }
}
