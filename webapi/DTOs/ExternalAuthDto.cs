using System;
namespace webapi.DTOs
{
	public class ExternalAuthDto
	{
		public string Provider { get; set; }

		public string IdToken { get; set; }
	}
}

