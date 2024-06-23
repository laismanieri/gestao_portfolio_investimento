using GestaoPortfolioInvestimento.Data;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Services
{
    public class TransacaoService : ITransacao
    {
        private readonly DataContext _context;

        public TransacaoService(DataContext context)
        {
            _context = context;
        }

        public List<Transacao> ObterTodasTransacoes(int skip, int take)
        {
            return _context.Transacoes.Skip(skip).Take(take).ToList();
        }

        public Transacao ObterTransacaoPorId(int id)
        {
            var transacao = _context.Transacoes.FirstOrDefault(t => t.ID == id);

            if (transacao == null)
            {
                throw new KeyNotFoundException($"Transação com ID {transacao.ID} não encontrado.");
            }
            return transacao;
        }
    }
}
