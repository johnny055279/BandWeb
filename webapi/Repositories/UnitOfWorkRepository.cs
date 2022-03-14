using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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

        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly IConfiguration configuration;

        public UnitOfWorkRepository(DataContext dataContext, IMapper mapper, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.dataContext = dataContext;

            this.mapper = mapper;

            this.hostingEnvironment = hostingEnvironment;

            this.configuration = configuration;
        }

        public IUserRepository UserRepository => new UserRepository(dataContext);

        public ITicketRepository TicketRepository => new TicketRepository(dataContext, mapper);

        public IPostRepository PostRepository => new PostRepository(dataContext);

        public INewsRepository NewsRepository => new NewsRepository(dataContext, mapper, hostingEnvironment, configuration);

        public async Task<bool> Complete()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}

