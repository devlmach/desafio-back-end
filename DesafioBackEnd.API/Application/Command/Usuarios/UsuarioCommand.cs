using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Usuarios { }

    public abstract class UsuarioCommand : IRequest<Usuario>
    {
        public required string NomeCompleto { get; set; }

        public required string Cpf { get; set; }

        public required string Email { get; set; }

        public string? Senha { get; set; }

        public required UserType Tipo { get; set; }

        public required decimal Carteira { get; set; } = 0;

        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public required bool IsActive { get; set; } = true;
}



