using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using webapi.Helper;
using webapi.Models;

namespace webapi.Services
{
	public class QuartzHostedService : IHostedService
	{
        private readonly ISchedulerFactory schedulerFactory;

        private readonly IJobFactory jobFactory;

        private readonly ILogger<QuartzHostedService> logger;

        private readonly IEnumerable<JobScheduleModel> injectJobSchedules;

        private List<JobScheduleModel> allJobSchedules;

        public IScheduler Scheduler { get; set; }

        public CancellationToken CancellationToken { get; private set; }

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, ILogger<QuartzHostedService> logger, IEnumerable<JobScheduleModel> injectJobSchedules)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobFactory = jobFactory;
            this.logger = logger;
            this.injectJobSchedules = injectJobSchedules;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (Scheduler == null || Scheduler.IsShutdown)
            {
                // 存下 cancellation token 
                CancellationToken = cancellationToken;

                // 先加入在 startup 註冊注入的 Job 工作
                allJobSchedules = new List<JobScheduleModel>();
                allJobSchedules.AddRange(injectJobSchedules);

                // 動態加入新的Job
                // allJobSchedules.Add(new JobScheduleModel("example", typeof(ReportJob), "0/13 * * * * ?"));

                // 初始排程器 Scheduler
                Scheduler = await schedulerFactory.GetScheduler(cancellationToken);
                Scheduler.JobFactory = jobFactory;


                // 逐一將工作項目加入排程器中 
                foreach (var jobSchedule in allJobSchedules)
                {
                    var jobDetail = CreateJobDetail(jobSchedule);
                    var trigger = CreateTrigger(jobSchedule);
                    await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
                    jobSchedule.JobStatus = JobStatus.Scheduled;
                }

                // 啟動排程
                await Scheduler.Start(cancellationToken);
            }
        }

        public async Task<IEnumerable<JobScheduleModel>> GetJobSchedules()
        {
            if (Scheduler.IsShutdown)
            {
                // 排程器停止時更新各工作狀態為停止
                foreach (var jobSchedule in allJobSchedules)
                {
                    jobSchedule.JobStatus = JobStatus.Stopped;
                }
            }
            else
            {
                // 取得目前正在執行的 Job 來更新各 Job 狀態
                var executingJobs = await Scheduler.GetCurrentlyExecutingJobs();
                foreach (var jobSchedule in allJobSchedules)
                {
                    var isRunning = executingJobs.FirstOrDefault(j => j.JobDetail.Key.Name == jobSchedule.JobName) != null;
                    jobSchedule.JobStatus = isRunning ? JobStatus.Running : JobStatus.Scheduled;
                }

            }

            return allJobSchedules;
        }

        public async Task TriggerJobAsync(string jobName)
        {
            if (Scheduler != null && !Scheduler.IsShutdown)
            {
                logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - job{jobName} - TriggerJobAsync");
                await Scheduler.TriggerJob(new JobKey(jobName), CancellationToken);
            }
        }

        private async Task<IJobExecutionContext> GetExecutingJob(string jobName)
        {
            if (Scheduler != null)
            {
                var executingJobs = await Scheduler.GetCurrentlyExecutingJobs();
                return executingJobs.FirstOrDefault(j => j.JobDetail.Key.Name == jobName);
            }

            return null;
        }

        public async Task InterruptJobAsync(string jobName)
        {
            if (Scheduler != null && !Scheduler.IsShutdown)
            {
                var targetExecutingJob = await GetExecutingJob(jobName);
                if (targetExecutingJob != null)
                {
                    logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - job{jobName} - InterruptJobAsync");
                    await Scheduler.Interrupt(new JobKey(jobName));
                }

            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (Scheduler != null && !Scheduler.IsShutdown)
            {
                logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - Scheduler StopAsync");
                await Scheduler.Shutdown(cancellationToken);
            }
        }

        private IJobDetail CreateJobDetail(JobScheduleModel jobScheduleModel)
        {
            var jobType = jobScheduleModel.JobType;
            var jobDetail = JobBuilder
                .Create(jobType)
                .WithIdentity(jobScheduleModel.JobName)
                .WithDescription(jobType.Name)
                .Build();

            // 可以在建立 job 時傳入資料給 job 使用
            jobDetail.JobDataMap.Put("Payload", jobScheduleModel);

            return jobDetail;
        }

        private ITrigger CreateTrigger(JobScheduleModel jobScheduleModel)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{jobScheduleModel.JobName}.trigger")
                .WithCronSchedule(jobScheduleModel.CronExpression)
                .WithDescription(jobScheduleModel.CronExpression)
                .Build();
        }
    }
}

