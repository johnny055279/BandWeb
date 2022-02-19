using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using webapi.Data;
using webapi.DTOs;
using webapi.Entities;
using webapi.Extensions;
using webapi.Helper;
using webapi.Interfaces;

namespace webapi.Repositories
{
	public class TicketRepository : ITicketRepository
	{
        private readonly DataContext dataContext;

        private readonly IConnectionMultiplexer connectionMultiplexer;

        private readonly IMapper mapper;

        private readonly IDatabase database;

        public TicketRepository(DataContext dataContext, IMapper mapper, IConnectionMultiplexer connectionMultiplexer)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.connectionMultiplexer = connectionMultiplexer;
            this.database = connectionMultiplexer.GetDatabase(0);
        }

        public void CreateTicket(Ticket ticket)
        {
            database.StringSet($"ticket_{ticket.Id}", JsonSerializer.Serialize(ticket));

            dataContext.Add(ticket);
        }

        public async Task DeleteTicket(int id)
        {
            var ticket = await dataContext.Tickets.FindAsync(id);

            dataContext.Remove(ticket);

            database.KeyDelete($"ticket_{ticket.Id}");
        }

        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            var ticket = await dataContext.Tickets.Include(n => n.City).Where(n => n.Id == id).Select(n => new TicketDto
            {
                Id = n.Id,
                CityId = n.CityId,
                CityName = n.City.CityName,
                ShowTime = n.ShowTime,
                Price = n.Price,
                RemainNumber = n.RemainNumber,
                CompleteShow = n.CompleteShow,
                ImageUrl = n.ImageUrl,
                Open = n.Open,
                Title = n.Title,
                SubTitle = n.SubTitle,
                SoldOut = n.SoldOut,
                PurchaseDeadLine = n.PurchaseDeadLine
            }).SingleOrDefaultAsync();

            return ticket;
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsAsync(bool soldOut, bool completeShow)
        {
            var tickets =  await dataContext.Tickets.Include(n => n.City)
                .Where(n => n.SoldOut == soldOut && n.CompleteShow == completeShow)
                .Select( n => new TicketDto
                {
                    Id = n.Id,
                    CityId = n.CityId,
                    CityName = n.City.CityName,
                    ShowTime = n.ShowTime,
                    Price = n.Price,
                    RemainNumber = n.RemainNumber,
                    CompleteShow = n.CompleteShow,
                    ImageUrl = n.ImageUrl,
                    Open = n.Open,
                    Title = n.Title,
                    SubTitle = n.SubTitle,
                    SoldOut = n.SoldOut,
                    PurchaseDeadLine = n.PurchaseDeadLine
                })
                .OrderByDescending(n => n.ShowTime).ToListAsync();

            return mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task UpdateTicket(int id, TicketUpdateDto ticketUpdateDto)
        {
            var ticket = await dataContext.Tickets.FindAsync(id);

            mapper.Map(ticketUpdateDto, ticket);

            await database.StringSetAsync($"ticket_{id}", JsonSerializer.Serialize(ticket));

            dataContext.Entry<Ticket>(ticket).State = EntityState.Modified;
        }

        public async Task<bool> PurchaseTicket(int id, int number, AppUser appUser, IRedisService redisService)
        {
            var ticket = await dataContext.Tickets.FindAsync(id);

            if (!ticket.Open)
            {
                return false;
            }

            var result = await database.GetOrSetAsync(id, redisService.GetTicketAsync);

            database.StringDecrement($"ticket_{id}", number);

            ticket.RemainNumber -= number;

            dataContext.Entry<Ticket>(ticket).State = EntityState.Modified;

            var userTicketOrders = await dataContext.UserTicketOrders.Where(n => n.TicketId == id && n.UserId == appUser.Id).FirstOrDefaultAsync() ;

            if (userTicketOrders != null)
            {
                userTicketOrders.Number += number;

                dataContext.Entry<UserTicketOrder>(userTicketOrders).State = EntityState.Modified;
            }

            await dataContext.UserTicketOrders.AddAsync(new UserTicketOrder
            {
                Number = number,
                UserId = appUser.Id,
                TicketId = appUser.Id,
                User = appUser,
                Ticket = ticket
            });


            return true;
        }
    }
}

