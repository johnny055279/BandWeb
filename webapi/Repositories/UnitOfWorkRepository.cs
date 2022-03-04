using System;
using System.Threading.Tasks;
using AutoMapper;
using StackExchange.Redis;
using webapi.Data;
using webapi.Entities;
using webapi.Interfaces;

namespace webapi.Repositories
{
	public class UnitOfWorkRepository : IUnitOfWork
	{
		private readonly DataContext dataContext;

        private readonly IMapper mapper;

        public UnitOfWorkRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;

            this.mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(dataContext);

        public ITicketRepository TicketRepository => new TicketRepository(dataContext, mapper);

        public IPostRepository PostRepository => new PostRepository(dataContext);

        public INewsRepository NewsRepository => new NewsRepository(dataContext, mapper);

        public async Task<bool> Complete()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}

