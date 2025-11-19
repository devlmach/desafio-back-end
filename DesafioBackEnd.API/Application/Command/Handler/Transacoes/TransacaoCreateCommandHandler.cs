using DesafioBackEnd.API.Application.Command.Transacoes;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Transacoes
{
    public class TransacaoCreateCommandHandler : IRequestHandler<TransacaoCreateCommand, Transacao>
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoCreateCommandHandler(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository; 
        }

        public async Task<Transacao> Handle(TransacaoCreateCommand request, CancellationToken cancellationToken)
        {
            var transacao = new Transacao(request.IdSender, request.IdReceiver, request.QuantiaTransferida, request.CreatedAt);
            if (transacao == null)
            {
                throw new ApplicationException("error creating entity");
            }
            else
            {
                return await _transacaoRepository.CreateTransacaoAsync(transacao);
            }

        }
    }
}
