using DesafioBackEnd.API.Entity;
using DesafioBackEnd.API.Repository;

namespace DesafioBackEnd.API.Service
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
