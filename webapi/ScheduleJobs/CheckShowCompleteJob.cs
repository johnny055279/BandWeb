using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using webapi.Data;
using webapi.Entities;
using webapi.Interfaces;
using webapi.Models;

namespace webapi.ScheduleJobs
{
    // prevent execute jobs at same time
    [DisallowConcurrentExecution]
    internal class CheckShowCompleteJob : IJob
	{
        private readonly ILogger<CheckShowCompleteJob> logger;

        private readonly IServiceProvider provider;

        public CheckShowCompleteJob(ILogger<CheckShowCompleteJob> logger, IServiceProvider provider)
        {
            this.logger = logger;
            this.provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var schedule = context.JobDetail.JobDataMap.Get("Payload") as JobScheduleModel;

            var jobName = schedule.JobName;

            using (var scope = provider.CreateScope())
            {
                // 如果要使用到 DI 容器中定義為 Scope 的物件實體時，由於 Job 定義為 singleton
                // 因此無法直接取得 Scope 的實體，此時就需要於 CreateScope 在 scope 中產生該實體
                var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var tickets = await dataContext.Tickets.Where(n => n.ShowTime < DateTime.Now).ToListAsync();

                if (tickets.Count == 0)
                {
                    logger.LogInformation($"@No job dectected, Time: {DateTime.Now.ToLongDateString()}");

                    return;
                }

                logger.LogInformation($"@Job start: Job Name: {jobName}, Time: {DateTime.Now.ToLongDateString()}");

                foreach (var ticket in tickets)
                {
                    // 自己定義當 job 要被迫被被中斷時，哪邊適合結束
                    // 如果沒有設定，當作業被中斷時，並不會真的中斷，而會整個跑完
                    if (context.CancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    logger.LogInformation($"@Job working: Job Name: {jobName} - ticket {ticket.Id}, Time: {DateTime.Now.ToLongDateString()}");

                    ticket.CompleteShow = true;

                    dataContext.Entry<Ticket>(ticket).State = EntityState.Modified;

                }

                await unitOfWork.Complete();

                logger.LogInformation($"@Job finished: Job Name: {jobName}, Time: {DateTime.Now.ToLongDateString()}");
            }
        }
    }
}

