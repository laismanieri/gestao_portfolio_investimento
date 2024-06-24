using GestaoPortfolioInvestimento.Data;
using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolioInvestimento.Services
{
    public class ProdutoFinanceiroService : IProdutoFinanceiro
    {
        private readonly DataContext _context;

        public ProdutoFinanceiroService(DataContext context)
        {
            _context = context;
        }

        public void AdicionarProdutoFinanceiro(ProdutoFinanceiroDTO produtoFinanceiroDto)
        {
            if (produtoFinanceiroDto == null)
            {

                throw new ArgumentNullException(nameof(produtoFinanceiroDto), "O produto financeiro não pode ser nulo");
            }

            var novoProdutoFinanceiroDto = new ProdutoFinanceiro
            {
                Nome = produtoFinanceiroDto.Nome,
                Tipo = produtoFinanceiroDto.Tipo,   
                TaxaRetorno = produtoFinanceiroDto.TaxaRetorno,
                ValorCota = produtoFinanceiroDto.ValorCota
            };

            _context.ProdutosFinanceiros.Add(novoProdutoFinanceiroDto);
            _context.SaveChanges();
        }

        public void AtualizarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro)
        {
            _context.Entry(produtoFinanceiro).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public ProdutoFinanceiro ObterProdutoFinanceiroPorId(int id)
        {
            var produtoFinanceiro = _context.ProdutosFinanceiros.FirstOrDefault(pf => pf.ID == id);

            if (produtoFinanceiro == null)
            {
                throw new KeyNotFoundException($"Produto Financeiro com ID {produtoFinanceiro.ID} não encontrado.");
            }
            return produtoFinanceiro;
        }

        public List<ProdutoFinanceiro> ObterTodosProdutosFinanceiros(int skip, int take)
        {
            return _context.ProdutosFinanceiros.Skip(skip).Take(take).ToList();
        }

        public void RemoverProdutoFinanceiro(int id)
        {
            var produtoFinanceiro = ObterProdutoFinanceiroPorId(id);
            _context.ProdutosFinanceiros.Remove(produtoFinanceiro);
            _context.SaveChanges();
        }
    }
}
