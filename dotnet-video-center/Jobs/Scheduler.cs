using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

namespace GMM7.Jobs
{
    public class SchedulerJob
    {
        private readonly IConfiguration Configuration;
        IScheduler scheduler;

        public SchedulerJob(IConfiguration configuration)
        {
            Configuration = configuration;
            // get a scheduler
        }
        public async void Run()
        {

            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<FileScannerJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(1)
                    .RepeatForever())
            .Build();

            await scheduler.ScheduleJob(job, trigger);
            Log.Information("Job started");
        }
    }
}