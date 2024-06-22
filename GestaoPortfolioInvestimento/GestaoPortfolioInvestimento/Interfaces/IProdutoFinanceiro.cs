using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IProdutoFinanceiro
    {
        public void AdicionarProdutoFinanceiro(Investimento produtoFinanceiro);

        public ProdutoFinanceiro ObterProdutoFinanceiroPorId(int id);

        public List<ProdutoFinanceiro> ObterTodosProdutosFinanceiros();

        public void AtualizarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro);

        public void RemoverProdutoFinanceiro(int id);
    }
}

