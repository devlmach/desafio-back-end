using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Application.Dto.Model
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public required string ConfirmPassword { get; set; }
    }
}
