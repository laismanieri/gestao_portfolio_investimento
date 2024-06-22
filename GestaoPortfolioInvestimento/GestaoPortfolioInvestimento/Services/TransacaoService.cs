using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Services
{
    public class TransacaoService : ITransacao
    {
        private List<Transacao> transacoes = new List<Transacao>();
        public void AdicionarTransacao(Transacao transacao)
        {
            if (transacao == null)
            {

                throw new ArgumentNullException(nameof(transacao), "A transação não pode ser nula");
            }
            transacao.Data = DateTime.Now;
            transacoes.Add(transacao);
        }

        public List<Transacao> ObterTodasTransacoes(int skip, int take)
        {
            return transacoes.Skip(skip).Take(take).ToList();
        }

        public Transacao ObterTransacaoPorId(int id)
        {
            var transacao = transacoes.FirstOrDefault(t => t.ID == id);

            if (transacao == null)
            {
                throw new KeyNotFoundException($"Transação com ID {transacao.ID} não encontrado.");
            }
            return transacao;
        }
    }
}
