using AutoMapper;
using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Command.Transacoes;
using DesafioBackEnd.API.Application.Dto.Transacao;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TransacaoService(IMapper mapper, IMediator mediator, IUsuarioRepository usuarioRepository, ITransacaoRepository transacaoRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task CreateTransacaoAsync(CreateTransacaoDto createTransacaoDto)
        {
            var senderId = await _usuarioRepository.GetByIdAsync(createTransacaoDto.IdSender);
            var receiverId = await _usuarioRepository.GetByIdAsync(createTransacaoDto.IdReceiver);

            if (senderId == null || senderId.IsActive == false)
                throw new Exception("Sender user cannot be found.");

            if (receiverId == null || receiverId.IsActive == false)
                throw new Exception("Receiver user cannot be found.");

            if (senderId.Tipo == UserType.LOJISTA)
                throw new Exception("LOJISTAS cant make transfer");

            if (senderId.Carteira < createTransacaoDto.QuantiaTransferida || senderId.Carteira <= 0)
                throw new Exception("Sender has less then the quantity to complete the transfer");

            senderId.Carteira -= createTransacaoDto.QuantiaTransferida;
            receiverId.Carteira += createTransacaoDto.QuantiaTransferida;

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

        public async Task<IEnumerable<DetailTransacaoDto>> GetTransacoesAsync(int pageNumber, int pageSize)
        {
            var transacoesQuery = new GetTransacoesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            if (transacoesQuery ==  null)
                throw new Exception($"Entities could not be loaded");

            var result = await _mediator.Send(transacoesQuery);
            return _mapper.Map<IEnumerable<DetailTransacaoDto>>(result);
        }
    }
}
