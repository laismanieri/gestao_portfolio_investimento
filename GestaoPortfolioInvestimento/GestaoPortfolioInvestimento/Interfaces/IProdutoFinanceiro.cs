using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IProdutoFinanceiro
    {
        public void AdicionarProdutoFinanceiro(ProdutoFinanceiroDTO produtoFinanceiroDto);

        public ProdutoFinanceiro ObterProdutoFinanceiroPorId(int id);

        public List<ProdutoFinanceiro> ObterTodosProdutosFinanceiros(int skip, int take);

        public void AtualizarProdutoFinanceiro(int id, ProdutoFinanceiroDTO produtoFinanceiroDto);

        public void RemoverProdutoFinanceiro(int id);
    }
}

