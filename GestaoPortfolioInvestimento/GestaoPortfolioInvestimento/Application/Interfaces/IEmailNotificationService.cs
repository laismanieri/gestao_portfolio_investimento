using InvestmentPortfolioManagement.Application.DTOs;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IEmailNotificationService
    {
        Task SendUpcomingInvestmentsEmailAsync(Dictionary<int, List<InvestmentDetailDTO>> investmentsByClient, string toEmail);
    }
}