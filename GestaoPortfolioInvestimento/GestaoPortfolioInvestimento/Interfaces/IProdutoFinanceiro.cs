using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IProdutoFinanceiro
    {
        public void AdicionarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro);

        public ProdutoFinanceiro ObterProdutoFinanceiroPorId(int id);

        public List<ProdutoFinanceiro> ObterTodosProdutosFinanceiros(int skip, int take);

        public void AtualizarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro);

        public void RemoverProdutoFinanceiro(int id);
    }
}

