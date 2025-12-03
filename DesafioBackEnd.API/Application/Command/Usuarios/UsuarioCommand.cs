using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Usuarios 
{

    public abstract class UsuarioCommand : IRequest<Usuario>
    {
        public string? NomeCompleto { get; set; }

        public string? Cpf { get; set; }

        public string? Email { get; set; }

        public string? Senha { get; set; }

        public UserType Tipo { get; set; }

        public UserRole Role { get; set; }

        public decimal Carteira { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}



