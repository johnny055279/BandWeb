using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;

namespace webapi.Repositories
{
	public class TicketRepository : ITicketRepository
	{
        private readonly DataContext dataContext;

        private readonly IMapper mapper;

        public TicketRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public void CreateTicket(Ticket ticket)
        {
            dataContext.Add(ticket);
        }

        public async Task DeleteTicket(int id)
        {
            var ticket = await dataContext.Tickets.FindAsync(id);

            dataContext.Remove(ticket);
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await dataContext.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsAsync(bool soldOut, bool completeShow)
        {
            var tickets =  await dataContext.Tickets.Where(n => n.SoldOut == soldOut && n.CompleteShow == completeShow).ToListAsync();

            return mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task UpdateTicket(int id, TicketUpdateDto ticketUpdateDto)
        {
            var ticket = await dataContext.Tickets.FindAsync(id);

            mapper.Map(ticketUpdateDto, ticket);

            dataContext.Entry<Ticket>(ticket).State = EntityState.Modified;
        }
    }
}

