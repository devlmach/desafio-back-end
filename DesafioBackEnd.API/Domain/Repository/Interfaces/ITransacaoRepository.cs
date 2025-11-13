using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Domain.Repository.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
    }
}
