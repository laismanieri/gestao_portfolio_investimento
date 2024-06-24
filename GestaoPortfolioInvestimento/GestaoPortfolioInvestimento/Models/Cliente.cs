using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPortfolioInvestimento.Models
{
    public class Cliente
    {   
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        public string DataNascimento { get; set; }
        public string Endereco { get; set; }

        // Relacionamento 1:N
        public List<Investimento> Investimentos { get; set; } = new List<Investimento>();
    }
}
