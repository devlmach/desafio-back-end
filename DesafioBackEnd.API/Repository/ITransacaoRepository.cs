using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Repository
{
    public interface ITransacaoRepository
    {
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
    }
}
