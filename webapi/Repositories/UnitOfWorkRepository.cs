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

        private readonly IConnectionMultiplexer connectionMultiplexer;

        public UnitOfWorkRepository(DataContext dataContext, IMapper mapper, IConnectionMultiplexer connectionMultiplexer)
        {
            this.dataContext = dataContext;

            this.mapper = mapper;

            this.connectionMultiplexer = connectionMultiplexer;
        }

        public IUserRepository UserRepository => new UserRepository(dataContext);

        public ITicketRepository TicketRepository => new TicketRepository(dataContext, mapper, connectionMultiplexer);

        public IPostRepository PostRepository => new PostRepository(dataContext);

        public INewsRepository NewsRepository => new NewsRepository(dataContext, mapper);

        public async Task<bool> Complete()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}

