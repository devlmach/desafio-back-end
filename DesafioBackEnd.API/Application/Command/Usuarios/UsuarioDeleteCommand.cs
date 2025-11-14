using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Usuarios
{
    public class UsuarioDeleteCommand : IRequest<Usuario>
    {
        public long Id { get; set; }

        public UsuarioDeleteCommand(long id)
        {
            Id = id;
        }
    }
}
