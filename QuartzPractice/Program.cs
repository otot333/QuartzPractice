using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var qsT = new QsTest();
            qsT.Start();
            Console.ReadLine();

        }
    }

    class QsTest
    {
        public void Start()
        {
            // Grab the Scheduler instance from the Factory 
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<PrintStringJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);

            // some sleep to show what's happening
            Thread.Sleep(TimeSpan.FromSeconds(60));

            // and last shut down the scheduler when you are ready to close your program
            scheduler.Shutdown();
        }

        public void RegisterJob()
        {

        }
    }

    class PrintStringJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("QuartzTest....");
        }
    }
}
