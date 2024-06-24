using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.DTO
{
    public class TransacaoDTO
    {
        public DateTime Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
