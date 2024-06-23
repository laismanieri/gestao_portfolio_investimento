using GestaoPortfolioInvestimento.Data;
using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolioInvestimento.Services
{
    public class InvestimentoService : IInvestimento
    {
        private readonly DataContext _context;

        public InvestimentoService(DataContext context)
        {
            _context = context;
        }

        public void AdicionarInvestimento(InvestimentoDTO investimentoDto)
        {   
            if (investimentoDto == null) { 

                throw new ArgumentNullException(nameof(investimentoDto), "O investimento não pode ser nulo");
            }


            var produtoFinanceiro = _context.ProdutosFinanceiros.FirstOrDefault(pf => pf.ID == investimentoDto.ProdutoFinanceiroID);

            if (produtoFinanceiro == null)
            {
                throw new KeyNotFoundException($"Produto financeiro com ID {investimentoDto.ProdutoFinanceiroID} não encontrado.");
            }

            var valorTotal = produtoFinanceiro.ValorCota * investimentoDto.Quantidade;


            // Adicionar o investimento ao banco de dados
            var novoInvestimento = new Investimento
            {
                ClienteID = investimentoDto.ClienteID,
                ProdutoFinanceiroID = investimentoDto.ProdutoFinanceiroID,
                Prazo = investimentoDto.Prazo,
                Quantidade = investimentoDto.Quantidade,
                DataAdesao = investimentoDto.DataAdesao,
                DataVenda = DateTime.Now, // Deixe a data de venda como nula
                Vencimento = investimentoDto.DataAdesao.AddDays(investimentoDto.Prazo % 365), // Usar o resto da divisão por 365 para evitar estouro do limite de datas
                ValorTotal = 0, // Deixe o valor total como zero
                Rendimento = 0
            };

            _context.Investimentos.Add(novoInvestimento);
            _context.SaveChanges();

            // Atualizar o investimento com os valores calculados
            novoInvestimento.ValorTotal = novoInvestimento.ProdutoFinanceiro.ValorCota * novoInvestimento.Quantidade;
            novoInvestimento.Vencimento = novoInvestimento.DataAdesao.AddDays(investimentoDto.Prazo % 365); // Atualizar novamente o vencimento com o valor calculado

            _context.SaveChanges();

            var novaTransacao = new Transacao
            {
                InvestimentoID = novoInvestimento.ID,
                Quantidade = novoInvestimento.Quantidade,
                Data = DateTime.Now,
                ValorUnitario = produtoFinanceiro.ValorCota,
                ValorTotal = novoInvestimento.ValorTotal,
                TipoTransacao = TipoTransacao.COMPRA
            };

            _context.Transacoes.Add(novaTransacao);
            _context.SaveChanges();
        }

        //public void AtualizarInvestimentoCompra(int id, CompraInvestimentoDTO compraInvestimentoDto)
        //{
        //    var investimento = _context.Investimentos.Include(i => i.ProdutoFinanceiro).FirstOrDefault(i => i.ID == id);
        //    if (investimento == null)
        //    {
        //        throw new KeyNotFoundException($"Investimento com ID {id} não encontrado.");
        //    }


        //    decimal valorCota = investimento.ProdutoFinanceiro.ValorCota;

        //    investimento.ValorTotal += valorCota * compraInvestimentoDto.Quantidade;

        //    var novaTransacao = new Transacao
        //    {
        //        InvestimentoID = investimento.ID,
        //        Data = DateTime.Now,
        //        ValorUnitario = investimento.ProdutoFinanceiro.ValorCota,
        //        Quantidade = compraInvestimentoDto.Quantidade,
        //        ValorTotal= valorCota * compraInvestimentoDto.Quantidade,
        //        TipoTransacao = TipoTransacao.COMPRA
        //    };

        //    _context.Transacoes.Add(novaTransacao);

        //    _context.SaveChanges();
        //}

        public void AtualizarInvestimentoVenda(int id, VendaInvestimentoDTO vendaInvestimentoDto)
        {
            var investimento = _context.Investimentos.Include(i => i.ProdutoFinanceiro).FirstOrDefault(i => i.ID == id);
            if (investimento == null)
            {
                throw new KeyNotFoundException($"Investimento com ID {id} não encontrado.");
            }

            if (vendaInvestimentoDto.Quantidade > investimento.Quantidade)
            {
                throw new InvalidOperationException("Quantidade de venda excede a quantidade disponível.");
            }

            decimal valorCota = investimento.ProdutoFinanceiro.ValorCota;
            investimento.Quantidade -= vendaInvestimentoDto.Quantidade;
            investimento.ValorTotal -= valorCota * vendaInvestimentoDto.Quantidade;

            if (investimento.Quantidade == 0)
            {
                investimento.DataVenda = DateTime.Now;
            }

            var novaTransacao = new Transacao
            {
                InvestimentoID = investimento.ID,
                Data = DateTime.Now,
                Quantidade = vendaInvestimentoDto.Quantidade,
                ValorUnitario = investimento.ProdutoFinanceiro.ValorCota,
                ValorTotal = valorCota * vendaInvestimentoDto.Quantidade,
                TipoTransacao = TipoTransacao.VENDA
            };

            _context.Transacoes.Add(novaTransacao);
            _context.Entry(investimento).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Investimento ObterInvestimentoPorId(int id)
        {
            var investimento = _context.Investimentos.FirstOrDefault(i => i.ID == id);

            if (investimento == null)
            {
                throw new KeyNotFoundException($"Investimento com ID {investimento.ID} não encontrado.");
            }
            return investimento;
        }

        public List<Investimento> ObterTodosInvestimentos(int skip, int take)
        {
            return _context.Investimentos.Skip(skip).Take(take).ToList();
        }

        public void RemoverInvestimento(int id)
        {
            var investimento = ObterInvestimentoPorId(id);
            _context.Investimentos.Remove(investimento);
            _context.SaveChanges();
        }
    }
}
