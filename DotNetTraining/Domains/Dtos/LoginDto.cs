﻿using System.ComponentModel.DataAnnotations;

namespace DotNetTraining.Domains.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
