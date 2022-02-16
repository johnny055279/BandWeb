using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Entities
{
	[Table("Ticket")]
	public class Ticket
	{
        public Ticket(DateTime showTime, int cityId, decimal price, string title, string subTitle, int remainNumber)
        {
            ShowTime = showTime;
            CityId = cityId;
            Price = price;
            Title = title;
            SubTitle = subTitle;
            RemainNumber = remainNumber;
        }

        [Key]
		public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ShowTime { get; set; }

        public int CityId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public int RemainNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDeadLine { get; set; }

        public bool SoldOut { get; set; } = false;

        public bool CompleteShow { get; set; } = false;

        public bool Open { get; set; } = false;

        public string ImageUrl { get; set; }

        public ICollection<UserTicketOrder> TicketOrders { get; set; }

        public City City { get; set; }
    }
}

