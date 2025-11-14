using DesafioBackEnd.API.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Application.Dto.Usuario
{
    public class CreateUsuarioDto
    {
        [Required(ErrorMessage = "O Campo {0} não pode ser vazio")]
        public required string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O {0} deve ter {1} números")]
        public required string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser nulo")]
        public required string Email { get; set; }

        public string? Senha { get; set; }

        [Required(ErrorMessage = "O tipo do usuário não pode ser nulo")]
        public required UserType Tipo { get; set; }
    }
}
