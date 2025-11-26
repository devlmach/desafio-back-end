using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Dto.Transacao;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Domain.Errors;
using DesafioBackEnd.API.Integrations.Authorization.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IAuthorizationService _authorizationService;

        public TransacaoController(ITransacaoService transacaoService, IAuthorizationService authorizationService)
        {
            _transacaoService = transacaoService;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Endpoint para criar uma transação
        /// </summary>
        /// <param name="transacao"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/transfer")]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransacaoDto transacao)
        {
            if (transacao == null)
                return BadRequest();

            bool autorizado = await _authorizationService.IsAuthorizedAsync();

            if (!autorizado)
                throw new BadRequestException("Denied transaction");

            await _transacaoService.CreateTransacaoAsync(transacao);
            return Ok(transacao);
        }

        /// <summary>
        /// Endpoint para retornar transação pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailTransacaoDto>> GetById(long? id)
        {
            var transacao = await _transacaoService.GetTransacaoByIdAsync(id);
            if (transacao == null)
                throw new NotFoundException($"Transaction with id {id} not found");

            return Ok(transacao);
        }

        /// <summary>
        /// Endpoint para retornar todas as transações
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailTransacaoDto>>> GetAllTransacoes([FromQuery] QueryTransacaoParameter queryParameter)
        {
            var transacoes = await _transacaoService.GetTransacoesAsync(queryParameter.PageNumber, queryParameter.PageSize);
            return Ok(transacoes);
        }
    }
}
