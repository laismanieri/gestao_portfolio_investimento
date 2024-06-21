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

        public List<Transacao> ObterTodosTransacao()
        {
            return transacoes;
        }

        public Transacao ObterTransacaoPorID(int id)
        {
            var transacao = transacoes.FirstOrDefault(i => i.ID == id);

            if (transacao == null)
            {
                throw new KeyNotFoundException($"Transação com ID {transacao.ID} não encontrado.");
            }
            return transacao;
        }
    }
}
