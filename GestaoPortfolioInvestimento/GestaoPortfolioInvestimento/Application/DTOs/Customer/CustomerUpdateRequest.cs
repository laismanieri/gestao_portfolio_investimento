using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.Customer
{
    public class CustomerUpdateRequest

    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string? Email { get; set; }

        public string? DateOfBirth { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string? Address { get; set; }
    }
}
