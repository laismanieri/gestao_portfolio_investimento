using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Infrastructure.Services;
using Quartz;

namespace InvestmentPortfolioManagement.Infrastructure.Jobs
{
    public class SendUpcomingInvestmentsEmailJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SendUpcomingInvestmentsEmailJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var investmentService = scope.ServiceProvider.GetRequiredService<IInvestmentService>();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                // Define how many days before maturity to consider as "upcoming"
                int daysBeforeMaturity = 7;

                var investmentsByCustomer = investmentService.ListInvestmentsNearMaturity(daysBeforeMaturity);
                await emailService.SendUpcomingInvestmentsEmailAsync(investmentsByCustomer, "laismanieri@alunos.utfpr.edu.br");
            }
        }
    }
}