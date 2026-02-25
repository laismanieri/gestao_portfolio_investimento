using InvestmentPortfolioManagement.Application.Interfaces;
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
                var investmentService = scope.ServiceProvider.GetRequiredService<IFinancialProductService>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailNotificationService>();

                // Define how many days before maturity to consider as "upcoming"
                int daysBeforeMaturity = 7;

                var upcomingProducts = await investmentService.GetProductsNearMaturityAsync(daysBeforeMaturity);
                if (!upcomingProducts.Any())
                    return;

                await emailService.SendUpcomingProductsEmailAsync(upcomingProducts, "laismanieri@alunos.utfpr.edu.br");
                //await emailService.SendUpcomingInvestmentsEmailAsync(investmentsByCustomer, "laismanieri@alunos.utfpr.edu.br");
            }
        }
    }
}