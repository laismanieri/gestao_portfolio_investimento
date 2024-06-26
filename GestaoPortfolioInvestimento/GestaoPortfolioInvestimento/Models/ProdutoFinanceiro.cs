﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPortfolioInvestimento.Models
{
    public class ProdutoFinanceiro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome do produto financeiro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto financeiro não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo do produto financeiro é obrigatório.")]
        public TipoProdutoFinanceiro Tipo { get; set; }

        [Required(ErrorMessage = "O valor do produto financeiro é obrigatório.")]
        public decimal ValorCota  { get; set; }

        [Required(ErrorMessage = "A taxa de retorno do produto financeiro é obrigatório.")]
        public decimal TaxaRetorno { get; set; }

        // Relacionamento 1:N
        public List<Investimento> Investimentos { get; set; } = new List<Investimento>();
    }
}