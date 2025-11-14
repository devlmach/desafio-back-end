using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class GetUsuarioByIdQuery : IRequest<Usuario>
    {
        public long Id { get; set; }

        public GetUsuarioByIdQuery(long id)
        {
            Id = id;
        }
    }
}
