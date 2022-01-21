using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Entities
{
	[Table("Ticket")]
	public class Ticket
	{
        public Ticket(DateTime showTime, string location, decimal price, string title, string subTitle, int remainNumber)
        {
            ShowTime = showTime;
            Location = location;
            Price = price;
            Title = title;
            SubTitle = subTitle;
            RemainNumber = remainNumber;
        }

        [Key]
		public int Id { get; set; }

        public DateTime ShowTime { get; set; }

        public string Location { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public int RemainNumber { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public bool SoldOut { get; set; } = false;

        public bool CompleteShow { get; set; } = false;
    }
}

