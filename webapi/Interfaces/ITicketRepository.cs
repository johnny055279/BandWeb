using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DTOs;
using webapi.Entities;

namespace webapi.Interfaces
{
	public interface ITicketRepository
	{
		void CreateTicket(Ticket ticket);

		Task DeleteTicket(int id);

		Task UpdateTicket(TicketDto ticketDto);

		Task<IEnumerable<TicketDto>> GetTicketsAsync(bool soldOut, bool completeShow);

		Task<Ticket> GetTicketByIdAsync(int id);
	}
}

