using DesafioBackEnd.API.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Application.Dto.Usuarios
{
    public class DetailUsuarioDto
    {
        [Key]
        public long Id { get; set; }

        public required string NomeCompleto { get; set; }

        public required string Cpf { get; set; }

        public required string Email { get; set; }

        public string? Senha { get; set; }

        public required UserType Tipo { get; set; }

        public decimal Carteira { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
