using System;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface ITransacao
    {
        public void AdicionarTransacao(Transacao transacao);

        public Transacao ObterTransacaoPorID(int id);

        public List<Transacao> ObterTodosTransacao();
    }
}
