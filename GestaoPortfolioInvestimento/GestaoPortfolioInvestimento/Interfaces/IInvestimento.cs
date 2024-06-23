using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IInvestimento
    {   
        public void AdicionarInvestimento(InvestimentoDTO investimentoDto);

        public Investimento ObterInvestimentoPorId(int id);

        public List<Investimento> ObterInvestimentoPorClienteId(int id);

        public List<Investimento> ObterTodosInvestimentos(int skip, int take);

        //void AtualizarInvestimentoCompra(int id, CompraInvestimentoDTO compraInvestimentoDto);

        public void AtualizarInvestimentoVenda(int id, VendaInvestimentoDTO vendaInvestimentoDto);

        public void RemoverInvestimento(int id);

    }
}
