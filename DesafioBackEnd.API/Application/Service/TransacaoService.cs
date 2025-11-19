using AutoMapper;
using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Command.Transacoes;
using DesafioBackEnd.API.Application.Dto.Transacao;
using DesafioBackEnd.API.Application.Service.Interfaces;
using MediatR;

namespace DesafioBackEnd.API.Application.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TransacaoService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task CreateTransacaoAsync(CreateTransacaoDto createTransacaoDto)
        {
            var transacaoCreateCommand = _mapper.Map<CreateTransacaoDto, TransacaoCreateCommand>(createTransacaoDto);
            await _mediator.Send(transacaoCreateCommand);
        }

        public async Task<DetailTransacaoDto> GetTransacaoByIdAsync(long? id)
        {
            var transacao = new GetTransacaoByIdQuery(id.Value);
            if (transacao == null)
                throw new Exception($"Entity could not be found");

            var result = await _mediator.Send(transacao);
            return _mapper.Map<DetailTransacaoDto>(result);
        }

        public Task<IEnumerable<DetailTransacaoDto>> GetTransacoesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
