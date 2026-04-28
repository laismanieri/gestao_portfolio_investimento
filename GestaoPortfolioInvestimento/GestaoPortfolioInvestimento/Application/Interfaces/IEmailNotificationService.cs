using InvestmentPortfolioManagement.Application.DTOs.Notifications;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IEmailNotificationService
    {
        Task SendUpcomingProductsEmailAsync(List<UpcomingFinancialProductNotification> products,string toEmail);
    }
}