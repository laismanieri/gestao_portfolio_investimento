using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GestaoPortfolioInvestimento.Services
{
    public class EmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey ?? throw new ArgumentNullException(nameof(sendGridApiKey));
        }

        public async Task EnviarEmail(string destinatario, string assunto, string corpo)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("lais.manieri@hotmail.com", "Lais Manieri Coimbra");
            var to = new EmailAddress(destinatario);
            var msg = MailHelper.CreateSingleEmail(from, to, assunto, corpo, corpo);

            try
            {
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"E-mail enviado para {destinatario}. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            }
        }
    }
}
