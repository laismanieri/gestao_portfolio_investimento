using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IInvestimento
    {   
        public void AdicionarInvestimento(InvestimentoDTO investimentoDto);

        public Investimento ObterInvestimentoPorId(int id);

        public List<Investimento> ObterInvestimentoPorClienteId(int id);

        public ExtratoClienteDTO ObterExtratoPorClienteId(int clienteId);

        public List<Investimento> ObterTodosInvestimentos(int skip, int take);

        public Dictionary<int, List<InvestimentoDetalheDTO>> ListarInvestimentosPorProdutoFinanceiro();        

        public Dictionary<int, List<InvestimentoDetalheDTO>> ListarInvestimentosVencimentoProximo(int dias);
        //void AtualizarInvestimentoCompra(int id, CompraInvestimentoDTO compraInvestimentoDto);

        public void AtualizarInvestimentoVenda(int id, VendaInvestimentoDTO vendaInvestimentoDto);

        public void RemoverInvestimento(int id);

    }
}
