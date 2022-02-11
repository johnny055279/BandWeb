using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using webapi.Helper;
using webapi.Models;
using webapi.ScheduleJobs;
using webapi.Services;

namespace webapi.Extensions
{
	public static class AppScheduleExtensions
	{
		public static IServiceCollection AddAppScheduleExtensions(this IServiceCollection services)
		{
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

