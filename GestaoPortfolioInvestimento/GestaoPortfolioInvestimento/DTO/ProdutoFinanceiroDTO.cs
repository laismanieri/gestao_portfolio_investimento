using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;

namespace GestaoPortfolioInvestimento.DTO
{
    public class ProdutoFinanceiroDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome do produto financeiro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto financeiro não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo do produto financeiro é obrigatório.")]
        public TipoProdutoFinanceiro Tipo { get; set; }

        [Required(ErrorMessage = "O valor do produto financeiro é obrigatório.")]
        public decimal ValorCota { get; set; }

        [Required(ErrorMessage = "A taxa de retorno do produto financeiro é obrigatório.")]
        public decimal TaxaRetorno { get; set; }
    }
}
