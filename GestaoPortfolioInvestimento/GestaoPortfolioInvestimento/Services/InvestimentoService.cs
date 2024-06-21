using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Services
{
    public class InvestimentoService : IInvestimento
    {
        private List<Investimento> investimentos = new List<Investimento>();

        public void AdicionarInvestimento(Investimento investimento)
        {   
            if (investimento == null) { 

                throw new ArgumentNullException(nameof(investimento), "O investimento não pode ser nulo");
            }

            investimentos.Add(investimento);

        }

        public void AtualizarInvestimento(Investimento investimento)
        {
            var index = investimentos.FindIndex(i => i.ID == investimento.ID);
            if (index == -1)
            {
                throw new KeyNotFoundException($"Investimento com ID {investimento.ID} não encontrado.");
            }

            investimentos[index] = investimento;
        }

        public Investimento ObterInvestimentoPorId(int id)
        {
            var investimento = investimentos.FirstOrDefault(i => i.ID == id);

            if (investimento == null)
            {
                throw new KeyNotFoundException($"Investimento com ID {investimento.ID} não encontrado.");
            }
            return investimento;
        }

        public List<Investimento> ObterTodosInvestimentos()
        {
            return investimentos;
        }

        public void RemoverInvestimento(int id)
        {
            var investimento = ObterInvestimentoPorId(id);
            if (investimento != null)
            {
                throw new KeyNotFoundException($"Investimento com ID {investimento.ID} não encontrado.");
            }
            investimentos.Remove(investimento);
        }
    }
}
