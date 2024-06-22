using System.ComponentModel.DataAnnotations;

namespace GestaoPortfolioInvestimento.Models
{
    public class Investimento
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public int ProdutoFinanceiroID { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
        public DateTime DataCompra { get; set; }

        [Required(ErrorMessage = "O valor do investimento é obrigatório.")]
        public decimal ValorCompra { get; set; }

        [Required(ErrorMessage = "A data de vencimento do investimento é obrigatória.")]
        public DateTime Vencimento { get; set; }

        // Relacionamentos
        public Cliente Cliente { get; set; }
        public ProdutoFinanceiro ProdutoFinanceiro { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

    }
}