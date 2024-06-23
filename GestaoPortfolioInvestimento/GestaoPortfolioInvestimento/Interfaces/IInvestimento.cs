using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IInvestimento
    {   
        public void AdicionarInvestimento(InvestimentoDTO investimentoDto);

        public Investimento ObterInvestimentoPorId(int id);

        public List<Investimento> ObterTodosInvestimentos(int skip, int take);

        public void AtualizarInvestimento(Investimento investimento);

        public void RemoverInvestimento(int id);

    }
}
