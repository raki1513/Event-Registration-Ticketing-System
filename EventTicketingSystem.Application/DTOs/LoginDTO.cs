﻿using System.ComponentModel.DataAnnotations;

namespace EventTicketingSystem.Application.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
