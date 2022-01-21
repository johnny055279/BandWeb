using System;
using System.Threading.Tasks;

namespace webapi.Interfaces
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; }

		ITicketRepository TicketRepository { get; }

		Task<bool> Complete();
    }
}

