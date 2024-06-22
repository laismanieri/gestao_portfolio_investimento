using System;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface ITransacao
    {
        public void AdicionarTransacao(Transacao transacao);

        public Transacao ObterTransacaoPorId(int id);

        public List<Transacao> ObterTodasTransacoes(int skip, int take);
    }
}
