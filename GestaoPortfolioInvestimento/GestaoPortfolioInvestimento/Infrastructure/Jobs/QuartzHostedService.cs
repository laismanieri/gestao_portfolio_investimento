using Quartz.Spi;
using Quartz;

namespace InvestmentPortfolioManagement.Infrastructure.Jobs
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

            var job = JobBuilder.Create<SendUpcomingInvestmentsEmailJob>()
                .WithIdentity("SendUpcomingInvestmentsEmailJob", "group1")
                .Build();

            // Example Cron: executes daily at 7:02 PM
            var trigger = TriggerBuilder.Create()
                .WithIdentity("SendUpcomingInvestmentsEmailTrigger", "group1")
                .StartNow()
                .WithCronSchedule("0 02 19 * * ?") // executes daily at 19:02
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