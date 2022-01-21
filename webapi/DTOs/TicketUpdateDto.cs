using System;
namespace webapi.DTOs
{
	public class TicketUpdateDto
	{
		public DateTime ShowTime { get; set; }

		public string Location { get; set; }

		public decimal Price { get; set; }

		public string Title { get; set; }

		public string SubTitle { get; set; }
	}
}

