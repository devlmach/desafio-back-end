using AutoMapper;
using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Command.Transacoes;
using DesafioBackEnd.API.Application.Dto.Transacoes;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DesafioBackEnd.API.Application.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        //private readonly ITransacaoRepository _transacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransacaoService(IMapper mapper, IMediator mediator, IUsuarioRepository usuarioRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            //_transacaoRepository = transacaoRepository;
        }

        public async Task CreateTransacaoAsync(CreateTransacaoDto createTransacaoDto)
        {
            var idSender = long.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("UserId")!.Value);
            var sender = await _usuarioRepository.GetByIdAsync(idSender);

            var receiverId = await _usuarioRepository.GetByIdAsync(createTransacaoDto.IdReceiver);

            if (sender == null || sender.IsActive == false)
                throw new Exception("Sender user cannot be found.");

            if (receiverId == null || receiverId.IsActive == false)
                throw new Exception("Receiver user cannot be found.");

            if (sender.Tipo == UserType.LOJISTA)
                throw new Exception("LOJISTAS cant make transfer");

            if (sender.Carteira < createTransacaoDto.QuantiaTransferida || sender.Carteira <= 0)
                throw new Exception("Sender has less then the quantity to complete the transfer");

            sender.Carteira -= createTransacaoDto.QuantiaTransferida;
            receiverId.Carteira += createTransacaoDto.QuantiaTransferida;

            await _usuarioRepository.UpdateAsync(sender);
            await _usuarioRepository.UpdateAsync(receiverId);

            var transacaoCreateCommand = _mapper.Map<CreateTransacaoDto, TransacaoCreateCommand>(createTransacaoDto);
            transacaoCreateCommand.IdSender = idSender;

            try
            {
                await _mediator.Send(transacaoCreateCommand);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw;
            }
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
