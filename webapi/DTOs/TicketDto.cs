using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace webapi.DTOs
{
	public class TicketDto
	{
		public int Id { get; set; }
		[Required]
		public DateTime ShowTime { get; set; }
		[Required]
		public int CityId { get; set; }

		public string CityName { get; set; }

		[Required]
		public decimal Price { get; set; }
		[Required]
		[MaxLength(30)]
		public string Title { get; set; }
		[Required]
		[MaxLength(20)]
		public string SubTitle { get; set; }
		[Required]
		public int RemainNumber { get; set; }

		public bool SoldOut { get; set; }

		public bool CompleteShow { get; set; }
		[Required]
		public bool Open { get; set; }

		public string ImageName { get { return Image.FileName; } set { } }

		[Required]
		public IFormFile Image { get; set; }

		[Required]
		public DateTime PurchaseDeadLine { get; set; }
	}
}

