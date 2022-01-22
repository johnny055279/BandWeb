﻿using System;
using System.ComponentModel.DataAnnotations;
namespace webapi.DTOs
{
	public class TicketDto
	{
		public int Id { get; set; }
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

		public bool SoldOut { get; set; }

		public bool CompleteShow { get; set; }
		[Required]
		public bool Open { get; set; }
	}
}
