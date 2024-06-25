using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using Quartz;

namespace GestaoPortfolioInvestimento.Jobs
{
    public class EnviarEmailJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EnviarEmailJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var investimentoService = scope.ServiceProvider.GetRequiredService<IInvestimento>();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                // Defina quantos dias antes do vencimento você quer considerar como "próximo"
                int dias = 7;

                var investimentosPorCliente = investimentoService.ListarInvestimentosVencimentoProximo(dias);
                await emailService.EnviarEmailInvestimentosVencimentoProximoAsync(investimentosPorCliente, "laismanieri@alunos.utfpr.edu.br");
            }
        }
    }
}
