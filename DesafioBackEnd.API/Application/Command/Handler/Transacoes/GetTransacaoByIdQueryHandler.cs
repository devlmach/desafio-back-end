using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Transacoes
{
    public class GetTransacaoByIdQueryHandler : IRequestHandler<GetTransacaoByIdQuery, Transacao>
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public GetTransacaoByIdQueryHandler(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<Transacao> Handle(GetTransacaoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _transacaoRepository.GetByIdAsync(request.Id);
        }
    }
}
