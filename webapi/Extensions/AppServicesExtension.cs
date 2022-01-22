using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapi.Data;
using webapi.Helper;
using webapi.Interfaces;
using webapi.Service;
using System.Text.Json;
using webapi.Filters;
using Quartz.Spi;
using Quartz;
using webapi.Services;
using Quartz.Impl;
using webapi.Models;
using webapi.Repositories;
using webapi.ScheduleJobs;

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

            services.AddScoped<LogUserActivityFilter>();

            services.AddAutoMapper(typeof(AutoMapHelper).Assembly);

            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //向DI容器註冊Quartz服務
            services.AddSingleton<IJobFactory, JobFactoryHelper>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            //向DI容器註冊Job
            services.AddSingleton<CheckShowCompleteJob>();
            services.AddSingleton<CheckServerMemoryUsageJob>();

            //向DI容器註冊JobSchedule，這裡適合固定不變的作業
            services.AddSingleton(new JobScheduleModel("CheckShowCompleted", typeof(CheckShowCompleteJob), "0 0 23 * * ?"));

            services.AddSingleton(new JobScheduleModel("CheckServerMemoryUsage", typeof(CheckServerMemoryUsageJob), "0 */1 * * * ?"));

            //向DI容器註冊Host服務
            services.AddSingleton<QuartzHostedService>();
            services.AddHostedService(provider => provider.GetService<QuartzHostedService>());

            return services;
        }
    }
}

