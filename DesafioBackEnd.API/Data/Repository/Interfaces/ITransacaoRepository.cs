using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Data.Repository.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
    }
}
