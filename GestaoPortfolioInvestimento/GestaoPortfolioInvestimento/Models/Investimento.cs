using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPortfolioInvestimento.Models
{
    public class Investimento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public int ProdutoFinanceiroID { get; set; }

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

        // Relacionamentos
        [ForeignKey("ClienteID")]
        public Cliente Cliente { get; set; }

        [ForeignKey("ProdutoFinanceiroID")]
        public ProdutoFinanceiro ProdutoFinanceiro { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

    }
}