using DesafioBackEnd.API.Domain.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Service.Interfaces;
using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Domain.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<Transacao> CreateTransacaoAsync(Transacao transacao)
        {
            return await _transacaoRepository.CreateTransacaoAsync(transacao);
        }
    }
}
