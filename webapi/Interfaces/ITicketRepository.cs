using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DTOs;
using webapi.Entities;

namespace webapi.Interfaces
{
	public interface ITicketRepository
	{
		void CreateTicket(TicketDto ticketDto);

		Task DeleteTicket(int id);

		Task UpdateTicket(int id, TicketUpdateDto ticketUpdateDto);

		Task<IEnumerable<TicketDto>> GetTicketsAsync(bool soldOut, bool completeShow);

		Task<TicketDto> GetTicketByIdAsync(int id);

		Task<bool> PurchaseTicket(int id, int number, AppUser appUser, IRedisService redisService);
	}
}

