using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IInvestimento
    {   
        public void AdicionarInvestimento(Investimento investimento);

        public Investimento ObterInvestimentoPorId(int id);

        public List<Investimento> ObterTodosInvestimentos();

        public void AtualizarInvestimento(Investimento investimento);

        public void RemoverInvestimento(int id);
    }
}
