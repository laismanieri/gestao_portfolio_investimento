using GestaoPortfolioInvestimento.Data;
using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

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

        public List<Investimento> ObterInvestimentoPorClienteId(int clienteId)
        {
            var investimentos = _context.Investimentos
                .Where(i => i.ClienteID == clienteId)
                .ToList();

            if (investimentos == null || !investimentos.Any())
            {
                throw new KeyNotFoundException($"Nenhum investimento encontrado para o cliente com ID {clienteId}.");
            }

            return investimentos;
        }

        public ExtratoClienteDTO ObterExtratoPorClienteId(int clienteId)
        {
            var cliente = _context.Clientes.Include(c => c.Investimentos)
                                            .ThenInclude(i => i.ProdutoFinanceiro)
                                            .Include(c => c.Investimentos)
                                            .ThenInclude(i => i.Transacoes)
                                            .FirstOrDefault(c => c.ID == clienteId);

            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado");
            }

            var extrato = new ExtratoClienteDTO
            {
                ClienteID = cliente.ID,
                ClienteNome = cliente.Nome,
                Investimentos = cliente.Investimentos.Select(i => new InvestimentoDetalheDTO
                {
                    ProdutoFinanceiroNome = i.ProdutoFinanceiro.Nome,
                    Tipo = i.ProdutoFinanceiro.Tipo,
                    Quantidade = i.Quantidade,
                    ValorTotal = i.ValorTotal,
                    DataAdesao = i.DataAdesao,
                    DataVenda = i.DataVenda,
                    Vencimento = i.Vencimento,
                    Rendimento = i.Rendimento,
                    Transacoes = i.Transacoes.Select(t => new TransacaoDTO
                    {
                        Data = t.Data,
                        TipoTransacao = t.TipoTransacao,
                        Quantidade = t.Quantidade,
                        ValorTotal = t.ValorTotal
                    }).ToList()
                }).ToList()
            };

            return extrato;
        }

        public Investimento ObterInvestimentoPorId(int id)
        {
            var investimento = _context.Investimentos.FirstOrDefault(i => i.ID == id);

            if (investimento == null)
            {
                throw new KeyNotFoundException($"Investimento com ID {id} não encontrado.");
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

        public Dictionary<int, List<InvestimentoDetalheDTO>> ListarInvestimentosPorProdutoFinanceiro()
        {
            var investimentos = _context.Investimentos
                .Include(i => i.ProdutoFinanceiro)
                .Select(i => new InvestimentoDetalheDTO
                {
                    ID = i.ID,
                    ProdutoFinanceiroID = i.ProdutoFinanceiroID,
                    ProdutoFinanceiroNome = i.ProdutoFinanceiro.Nome,
                    Tipo = i.ProdutoFinanceiro.Tipo,
                    Quantidade = i.Quantidade,
                    ValorTotal = i.ValorTotal,
                    DataAdesao = i.DataAdesao,
                    DataVenda = i.DataVenda,
                    Vencimento = i.Vencimento,
                    TaxaRetorno = i.ProdutoFinanceiro.TaxaRetorno,
                    Rendimento = i.Rendimento
                })
                .ToList();

            // Agrupar os investimentos por ID do produto financeiro
            var investimentosAgrupados = investimentos
                .GroupBy(i => i.ProdutoFinanceiroID)
                .ToDictionary(g => g.Key, g => g.ToList());

            return investimentosAgrupados;
        }

        public Dictionary<int, List<InvestimentoDetalheDTO>> ListarInvestimentosVencimentoProximo(int dias)
        {
            DateTime dataLimite = DateTime.Now.AddDays(dias);

            var investimentos = _context.Investimentos
                .Include(i => i.ProdutoFinanceiro)
                .Include(i => i.Cliente)
                .Where(i => i.Vencimento <= dataLimite)
                .Select(i => new InvestimentoDetalheDTO
                {
                    ID = i.ID,
                    ClienteID = i.ClienteID,
                    ClienteNome = i.Cliente.Nome,
                    ClienteEmail = i.Cliente.Email,
                    ProdutoFinanceiroID = i.ProdutoFinanceiroID,
                    ProdutoFinanceiroNome = i.ProdutoFinanceiro.Nome,
                    Tipo = i.ProdutoFinanceiro.Tipo,
                    Quantidade = i.Quantidade,
                    ValorTotal = i.ValorTotal,
                    DataAdesao = i.DataAdesao,
                    DataVenda = i.DataVenda,
                    Vencimento = i.Vencimento,
                    TaxaRetorno = i.ProdutoFinanceiro.TaxaRetorno,
                    Rendimento = i.Rendimento
                })
                .ToList();

            // Agrupar os investimentos por ID do cliente
            var investimentosAgrupados = investimentos
                .GroupBy(i => i.ClienteID)
                .ToDictionary(g => g.Key, g => g.ToList());

            return investimentosAgrupados;
        }

    }
}
