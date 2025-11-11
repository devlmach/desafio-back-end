using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Service
{
    public interface ITransacaoService
    {
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
    }
}
