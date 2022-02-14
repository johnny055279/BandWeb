using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.DTOs
{
	public class NewsDto
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string SubTitle { get; set; }
		[Required]
		public string ImgUrl { get; set; }
		[Required]
		public string Content { get; set; }
	}
}

