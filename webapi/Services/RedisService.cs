using System;
using System.Threading.Tasks;
using webapi.Data;
using webapi.Entities;
using webapi.Interfaces;

namespace webapi.Services
{
	public class RedisService : IRedisService
	{
        private readonly DataContext dataContext;

        public RedisService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await dataContext.Posts.FindAsync(id);

            return post;
        }

        public async Task<Ticket> GetTicketAsync(int id)
        {
            var ticket = await dataContext.Tickets.FindAsync(id);

            return ticket;
        }
	}
}

