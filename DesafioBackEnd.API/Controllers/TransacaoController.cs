using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Dto.Transacoes;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Common.Middleware;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Domain.Errors;
using DesafioBackEnd.API.Integrations.Authorization.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IAuthorizationsService _authorizationService;

        public TransacaoController(ITransacaoService transacaoService, IAuthorizationsService authorizationService)
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
        [Authorize]
        [ProducesResponseType(typeof(DetailTransacaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(DetailTransacaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<DetailTransacaoDto>> GetById(long? id)
        {
            var user = TokenController.GetUser(Request);
        
            var transacao = await _transacaoService.GetTransacaoByIdAsync(id);

            if(User.IsInRole(UserRole.User.ToString()))
                if (transacao == null || transacao.IdSender != user.Id && transacao.IdReceiver != user.Id)
                {
                    throw new BadRequestException($"User typerole user cannot see other users transactions.");
                }
                else
                {
                    return Ok(transacao);
                }

            return Ok(transacao);  
        }

        /// <summary>
        /// Endpoint para retornar todas as transações de acordo com usuário autorizado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<DetailTransacaoDto>>> GetAllTransacoes([FromQuery] QueryTransacaoParameter queryParameter)
        {
            var userId = long.Parse(User.FindFirst("UserId")!.Value);

            IEnumerable<DetailTransacaoDto> transacoes;

            if (User.IsInRole(UserRole.Admin.ToString()))
            {
                transacoes = await _transacaoService.GetTransacoesAsync(queryParameter.PageNumber, queryParameter.PageSize);
            }
            else if (User.IsInRole(UserRole.User.ToString()))
            {
                transacoes = await _transacaoService.GetTransacoesUserAsync(userId, queryParameter.PageNumber, queryParameter.PageSize);
            }
            else
            {
                throw new ForbiddenException("Forbidden");
            }

            return Ok(transacoes);
        }
    }
}
