using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class GetTransacaoByIdQuery : IRequest<Transacao>
    {
        public long Id { get; set; }

        public GetTransacaoByIdQuery(long id)
        {
            Id = id;
        }
    }
}
