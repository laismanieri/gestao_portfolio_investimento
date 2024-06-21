using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{
    public interface IProdutoFinanceiro
    {
        public void AdicionarProdutoFinanceiro(Investimento produtoFinanceiro);

        public ProdutoFinanceiro ObterProdutoFinanceiroPorId(int id);

        public List<ProdutoFinanceiro> ObterTodosProdutoFinanceiro();

        public void AtualizarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro);

        public void RemoverProdutoFinanceiro(int id);
    }
}

