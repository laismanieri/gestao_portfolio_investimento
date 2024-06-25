using Quartz.Spi;
using Quartz;

namespace GestaoPortfolioInvestimento.Jobs
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private IScheduler _scheduler;

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            _scheduler.JobFactory = _jobFactory;
            await _scheduler.Start(cancellationToken);

            var job = JobBuilder.Create<EnviarEmailJob>()
                .WithIdentity("EnviarEmailJob", "group1")
                .Build();

            // .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 0)) // Executa diariamente às 9:00 AM
            //  .WithCronSchedule("0 05 19 * * ?")
            var trigger = TriggerBuilder.Create()
                .WithIdentity("EnviarEmailTrigger", "group1")
                .StartNow()
                .WithCronSchedule("0 02 19 * * ?") // Executa diariamente às 9:00 AM
                .Build();

            await _scheduler.ScheduleJob(job, trigger, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_scheduler != null)
            {
                await _scheduler.Shutdown(cancellationToken);
            }
        }
    }
}
