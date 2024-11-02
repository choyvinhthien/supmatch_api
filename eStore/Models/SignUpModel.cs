using System.ComponentModel.DataAnnotations;

namespace eStore.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string? ConfirmPassword { get; set; }
        [Required]
        public string RoleName { get; set; } = null!;
        public string? BankAccountNumber { get; set; } = null!;
        public string? Bank { get; set; } = null!;
    }
}
