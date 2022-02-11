using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapi.Data;
using webapi.Helper;
using webapi.Interfaces;
using webapi.Service;
using System.Text.Json;
using webapi.Repositories;

namespace webapi.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAppServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            // inject token services
            services.AddScoped<ITokenServices, TokenServices>();

            services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<ITicketRepository, TicketRepository>();

            services.AddScoped<IRedisService, RedisService>();

            services.AddAutoMapper(typeof(AutoMapHelper).Assembly);

            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}

