using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Text;
using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.Interfaces;

namespace InvestmentPortfolioManagement.Infrastructure.Services
{
    public class EmailService : IEmailNotificationService
    {
        private readonly SendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            string apiKey = _configuration["ApiKey"];
            _sendGridClient = new SendGridClient(apiKey);
        }

        public async Task SendUpcomingInvestmentsEmailAsync(Dictionary<int, List<InvestmentDetailDTO>> investmentsByCustomer, string toEmail)
        {
            var message = new SendGridMessage();
            message.SetFrom(new EmailAddress(_configuration["FromEmail"], _configuration["FromName"]));
            message.AddTo(toEmail);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Upcoming Investments:");
            sb.AppendLine();

            foreach (var investments in investmentsByCustomer.Values)
            {
                foreach (var investment in investments)
                {
                    sb.AppendLine($"Customer: {investment.CustomerName}");
                    sb.AppendLine($"Email: {investment.CustomerEmail}");
                    sb.AppendLine($"Financial Product: {investment.FinancialProductName}");
                    sb.AppendLine($"Product Type: {investment.FinancialProductType}");
                    sb.AppendLine($"Quantity: {investment.Quantity}");
                    sb.AppendLine($"Total Value: {investment.TotalValue:C}");
                    sb.AppendLine($"Subscription Date: {investment.SubscriptionDate:dd/MM/yyyy}");
                    sb.AppendLine($"Maturity Date: {investment.MaturityDate:dd/MM/yyyy}");
                    sb.AppendLine($"Return Rate: {investment.ReturnRate:P2}");
                    sb.AppendLine($"Yield: {investment.Yield:C}");
                    sb.AppendLine();
                }
            }

            message.Subject = "Upcoming Investment Maturities";
            message.PlainTextContent = sb.ToString();

            var response = await _sendGridClient.SendEmailAsync(message);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }
    }
}