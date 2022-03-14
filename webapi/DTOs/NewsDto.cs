using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace webapi.DTOs
{
	public class NewsDto
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string SubTitle { get; set; }

		public string ImageUrl { get; set; }
		[Required]
		[JsonIgnore]
		public IFormFile Image { get; set; }
		[Required]
		public string Content { get; set; }

		public DateTime PostDate { get; set; } = DateTime.Now;
	}
}

