using System;
using System.Threading.Tasks;

namespace webapi.Interfaces
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; }

		ITicketRepository TicketRepository { get; }

		IPostRepository PostRepository { get; }

		INewsRepository NewsRepository { get; }

		Task<bool> Complete();
    }
}

