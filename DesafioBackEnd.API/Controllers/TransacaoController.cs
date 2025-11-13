using DesafioBackEnd.API.Domain.Service.Interfaces;
using DesafioBackEnd.API.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;
        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        [Route("/transfer")]
        public async Task<ActionResult> CreateTransaction([FromBody] Transacao transacao)
        {
            if (transacao == null)
                return BadRequest();

            var result = await _transacaoService.CreateTransacaoAsync(transacao);

            return Ok(result);
        }


    }
}
