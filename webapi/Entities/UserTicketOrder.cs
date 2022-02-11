using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
	public class UserTicketOrder
	{
		public UserTicketOrder()
		{
		}

		[Key]
        public int Id { get; set; }

		public string type { get; set; }

		public int UserId { get; set; }

		public int TicketId { get; set; }

		public int Number { get; set; }

		public AppUser User { get; set; }

		public Ticket Ticket { get; set; }
    }
}

