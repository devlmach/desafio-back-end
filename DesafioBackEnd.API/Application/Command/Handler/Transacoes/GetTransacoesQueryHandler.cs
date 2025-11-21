using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Dto.Transacao;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Transacoes
{
    public class GetTransacoesQueryHandler : IRequestHandler<GetTransacoesQuery, IEnumerable<DetailTransacaoDto>>
    {
        private readonly ITransacaoRepository _transacaoRepository;
        public GetTransacoesQueryHandler(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository ?? throw new ArgumentNullException(nameof(transacaoRepository));
        }

        public async Task<IEnumerable<DetailTransacaoDto>> Handle(GetTransacoesQuery request, CancellationToken cancellationToken)
        {
            return await _transacaoRepository.GetTransacoesAsync(request.PageNumber, request.PageSize);
        }
    }
}
