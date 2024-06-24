using System.ComponentModel.DataAnnotations;

namespace GestaoPortfolioInvestimento.DTO
{
    public class InvestimentoDTO
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public int ProdutoFinanceiroID { get; set; }

        public int ProdutoFinanceiroNome { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
        public DateTime DataAdesao { get; set; }

        public DateTime? DataVenda { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal ValorTotal { get; set; }

        public decimal Rendimento { get; set; }

        [Required(ErrorMessage = "O prazo em dias é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O prazo em dias deve ser maior que zero.")]
        public int Prazo { get; set; }
    }
}
