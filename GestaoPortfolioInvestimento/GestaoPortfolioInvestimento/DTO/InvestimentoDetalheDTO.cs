using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.DTO
{
    public class InvestimentoDetalheDTO
    {
        public string ProdutoFinanceiroNome { get; set; }
        public TipoProdutoFinanceiro Tipo { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataAdesao { get; set; }
        public DateTime? DataVenda { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal TaxaRetorno { get; set; }
        public decimal Rendimento { get; set; }

        public List<TransacaoDTO> Transacoes { get; set; } = new List<TransacaoDTO>();
    }
}
