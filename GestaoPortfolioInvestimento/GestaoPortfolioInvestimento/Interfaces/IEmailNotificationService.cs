using InvestmentPortfolioManagement.DTO;

namespace InvestmentPortfolioManagement.Interfaces
{
    public interface IEmailNotificationService
    {
        Task SendUpcomingInvestmentsEmailAsync(Dictionary<int, List<InvestmentDetailDTO>> investmentsByClient, string toEmail);
    }
}