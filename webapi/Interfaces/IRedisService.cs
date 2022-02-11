using System;
using System.Threading.Tasks;
using webapi.Entities;

namespace webapi.Interfaces
{
	public interface IRedisService
	{
		public Task<Ticket> GetTicketAsync(int id);

		public Task<Post> GetPostAsync(int id);
	}
}

