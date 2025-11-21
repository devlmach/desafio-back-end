using DesafioBackEnd.API.Application.Dto.Transacao;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class GetTransacoesQuery : IRequest<IEnumerable<DetailTransacaoDto>>
    {
        public long? IdSender { get; set; }
        public long? IdReceiver { get; set; }
        public decimal? QuantiaTransferida { get; set; }
    }
}
