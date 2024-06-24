using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Text;

namespace GestaoPortfolioInvestimento.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            string apiKey = _configuration["ApiKey"];
            _sendGridClient = new SendGridClient(apiKey);
        }

        public async Task EnviarEmailInvestimentosVencimentoProximoAsync(Dictionary<int, List<InvestimentoDetalheDTO>> investimentosPorCliente, string toEmail)
        {
            var message = new SendGridMessage();
            message.SetFrom(new EmailAddress(_configuration["FromEmail"], _configuration["FromName"]));
            message.AddTo(toEmail);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Investimentos com Vencimento Próximo:");
            sb.AppendLine("");

            foreach (var investimentos in investimentosPorCliente.Values)
            {
                foreach (var investimento in investimentos)
                {
                    sb.AppendLine($"Cliente: {investimento.ClienteNome}");
                    sb.AppendLine($"Email: {investimento.ClienteEmail}");
                    sb.AppendLine($"Produto Financeiro: {investimento.ProdutoFinanceiroNome}");
                    sb.AppendLine($"Tipo do Produto Financeiro: {investimento.Tipo}");
                    sb.AppendLine($"Quantidade: {investimento.Quantidade}");
                    sb.AppendLine($"Valor Total: {investimento.ValorTotal}");
                    sb.AppendLine($"Data de Adesão: {investimento.DataAdesao}");
                    sb.AppendLine($"Data de Vencimento: {investimento.Vencimento}");
                    sb.AppendLine($"Taxa de Retorno: {investimento.TaxaRetorno}");
                    sb.AppendLine($"Rendimento: {investimento.Rendimento}");

                    sb.AppendLine();
                }
            }

            message.Subject = "Investimentos com Vencimento Próximo";
            message.PlainTextContent = sb.ToString();

            var response = await _sendGridClient.SendEmailAsync(message);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }
    }

}
