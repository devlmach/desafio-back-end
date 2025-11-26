using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Application.Dto.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public required string Role { get; set; }    
    }
}
