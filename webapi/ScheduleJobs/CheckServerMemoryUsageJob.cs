using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using webapi.Data;

namespace webapi.ScheduleJobs
{
    [DisallowConcurrentExecution]
	internal class CheckServerMemoryUsageJob : IJob
	{
        private readonly ILogger<CheckShowCompleteJob> logger;

        public CheckServerMemoryUsageJob(ILogger<CheckShowCompleteJob> logger)
        {
            this.logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        { 
            logger.LogInformation($"Memory Usage: {Process.GetCurrentProcess().WorkingSet64}");

            return Task.CompletedTask;
        }
    }
}

