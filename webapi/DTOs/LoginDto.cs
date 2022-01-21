using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.DTOs
{
	public class LoginDto
	{
        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

