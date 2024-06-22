using System.ComponentModel.DataAnnotations;

namespace GestaoPortfolioInvestimento.DTO
{
    public class ClienteDTO
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        public string DataNascimento { get; set; }
        public string Endereco { get; set; }

    }
}
