using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.DTOs
{
	public class TicketUpdateDto
	{ 
		[Required]
		public DateTime ShowTime { get; set; }
		[Required]
		public string Location { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string SubTitle { get; set; }
		[Required]
		public int RemainNumber { get; set; }
		[Required]
		public bool Open { get; set; }
	}
}

