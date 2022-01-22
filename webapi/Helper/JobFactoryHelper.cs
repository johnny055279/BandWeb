using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace webapi.Helper
{
	public class JobFactoryHelper : IJobFactory
	{
        private readonly IServiceProvider serviceProvider;

        public JobFactoryHelper(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;

            return serviceProvider.GetRequiredService(jobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;

            disposable?.Dispose();
        }
    }
}

