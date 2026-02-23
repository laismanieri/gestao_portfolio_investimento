using InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription;
using InvestmentPortfolioManagement.Application.DTOs.Notifications;
using InvestmentPortfolioManagement.Application.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Text;

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
           
        public async Task SendUpcomingProductsEmailAsync(List<UpcomingFinancialProductNotification> 
            products,
            string toEmail)
        {
            if (products == null || !products.Any())
                return;

            var message = new SendGridMessage();
            message.SetFrom(new EmailAddress(
                _configuration["FromEmail"],
                _configuration["FromName"]));

            message.AddTo(toEmail);

            var sb = new StringBuilder();
            sb.AppendLine("Financial Products Near Maturity:");
            sb.AppendLine();

            foreach (var product in products)
            {
                sb.AppendLine($"Product: {product.Name}");
                sb.AppendLine($"Type: {product.Type}");
                sb.AppendLine($"Maturity Date: {product.MaturityDate:dd/MM/yyyy}");
                sb.AppendLine($"Return Rate: {product.ReturnRate:P2}");
                sb.AppendLine("---");
            }

            message.Subject = "Upcoming Financial Product Maturities";
            message.PlainTextContent = sb.ToString();

            var response = await _sendGridClient.SendEmailAsync(message);

            if (response.StatusCode != HttpStatusCode.OK &&
                response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception(
                    $"Failed to send email. Status code: {response.StatusCode}");
            }
        }
    }
}
