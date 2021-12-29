﻿using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Data.Dtos
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
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords does not match.")]
        public string RePassword { get; set; }
    
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
