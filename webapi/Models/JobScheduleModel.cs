using System;
using System.ComponentModel;

namespace webapi.Models
{
    public class JobScheduleModel
    {
        public JobScheduleModel(string jobName, Type jobType, string cronExpression)
        {
            JobName = jobName;
            JobType = jobType;
            CronExpression = cronExpression;
        }

        public string JobName { get; private set; }
        public Type JobType { get; private set; }
        public string CronExpression { get; private set; }
        public JobStatus JobStatus { get; set; } = JobStatus.Init;
    }

    public enum JobStatus : byte
    {
        [Description("Initialize")]
        Init = 0,
        [Description("Scheduled")]
        Scheduled = 1,
        [Description("Running")]
        Running = 2,
        [Description("Stoped")]
        Stopped = 3,
    }
    
}

