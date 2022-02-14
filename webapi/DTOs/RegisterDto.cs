using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.DTOs
{
	public class RegisterDto
	{
		[Required]
		[StringLength(20)]
        public string UserName { get; set; }
		[Required]
		[StringLength(20)]
		public string NickName { get; set; }
		[Required]
		[StringLength(20, MinimumLength = 8)]
		public string Password { get; set; }
		[Required]
		public string Gender { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
    }
}

