﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}