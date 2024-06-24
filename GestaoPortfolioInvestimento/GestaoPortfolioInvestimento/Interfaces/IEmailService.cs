using GestaoPortfolioInvestimento.DTO;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailInvestimentosVencimentoProximoAsync(Dictionary<int, List<InvestimentoDetalheDTO>> investimentosPorCliente, string toEmail);
    }
}

