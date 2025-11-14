using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class GetUsuariosQuery : IRequest<IEnumerable<Usuario>>
    {
    }
}
