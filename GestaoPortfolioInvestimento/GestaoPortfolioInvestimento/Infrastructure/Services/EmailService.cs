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

        //public async Task SendUpcomingInvestmentsEmailAsync(Dictionary<Guid, List<CustomerSubscriptionDetailResponse>> 
        //    investmentsByCustomer,
        //    string toEmail)
        //{
        //    var message = new SendGridMessage();
        //    message.SetFrom(new EmailAddress(_configuration["FromEmail"], _configuration["FromName"]));
        //    message.AddTo(toEmail);

        //    var sb = new StringBuilder();
        //    sb.AppendLine("Upcoming Subscriptions:");

        //    foreach (var customerGroup in investmentsByCustomer.Values)
        //    {
        //        foreach (var detail in customerGroup)
        //        {
        //            var sub = detail.CustomerSubscription; 
        //            sb.AppendLine($"Customer: {sub.Customer.Name}");
        //            sb.AppendLine($"Email: {sub.Customer.Email}");
        //            sb.AppendLine($"Product: {sub.FinancialProduct.Name}");
        //            sb.AppendLine($"Type: {sub.FinancialProduct.Type.Name}");
        //            sb.AppendLine($"Quantity: {sub.Quantity}");
        //            sb.AppendLine($"Total Value: {sub.TotalValue:C}");
        //            sb.AppendLine($"Created: {sub.CreatedAt:dd/MM/yyyy}");
        //            sb.AppendLine($"Maturity: {sub.FinancialProduct.:dd/MM/yyyy}");
        //            sb.AppendLine($"Return Rate: {sub.FinancialProduct.ReturnRate:P2}");
        //            sb.AppendLine($"Yield: {sub.Yield:C}");
        //            sb.AppendLine($"Transactions: {detail.Transactions.Count}");
        //            sb.AppendLine("---");
        //        }
        //        sb.AppendLine();
        //    }

        //    message.Subject = "Upcoming Subscription Maturities";
        //    message.PlainTextContent = sb.ToString();

        //    var response = await _sendGridClient.SendEmailAsync(message);
        //    if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
        //        throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
        //}

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
