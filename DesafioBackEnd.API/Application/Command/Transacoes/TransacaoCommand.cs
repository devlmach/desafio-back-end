using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Transacoes
{
    public class TransacaoCommand : IRequest<Transacao>
    {
        public long? IdSender { get; set; }
        public long? IdReceiver { get; set; }
        public decimal QuantiaTransferida { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
