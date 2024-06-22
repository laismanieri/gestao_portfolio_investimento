using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Services
{
    public class ProdutoFinanceiroService : IProdutoFinanceiro
    {
        private List<ProdutoFinanceiro> produtosFinanceiros = new List<ProdutoFinanceiro>();

        public void AdicionarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro)
        {
            if (produtoFinanceiro == null)
            {

                throw new ArgumentNullException(nameof(produtoFinanceiro), "O produto financeiro não pode ser nulo");
            }

            produtosFinanceiros.Add(produtoFinanceiro);
        }

        public void AdicionarProdutoFinanceiro(Investimento produtoFinanceiro)
        {
            throw new NotImplementedException();
        }

        public void AtualizarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro)
        {
            var index = produtosFinanceiros.FindIndex(i => i.ID == produtoFinanceiro.ID);
            if (index == -1)
            {
                throw new KeyNotFoundException($"Produto Financeiro com ID {produtoFinanceiro.ID} não encontrado.");
            }

            produtosFinanceiros[index] = produtoFinanceiro;
        }

        public ProdutoFinanceiro ObterProdutoFinanceiroPorId(int id)
        {
            var produtoFinanceiro = produtosFinanceiros.FirstOrDefault(pf => pf.ID == id);

            if (produtoFinanceiro == null)
            {
                throw new KeyNotFoundException($"Produto Financeiro com ID {produtoFinanceiro.ID} não encontrado.");
            }
            return produtoFinanceiro;
        }

        public List<ProdutoFinanceiro> ObterTodosProdutosFinanceiros()
        {
            return produtosFinanceiros;
        }

        public void RemoverProdutoFinanceiro(int id)
        {
            var produtoFinanceiro = ObterProdutoFinanceiroPorId(id);
            if (produtoFinanceiro != null)
            {
                throw new KeyNotFoundException($"Produto Financeiro com ID {produtoFinanceiro.ID} não encontrado.");
            }
            produtosFinanceiros.Remove(produtoFinanceiro);
        }
    }
}
