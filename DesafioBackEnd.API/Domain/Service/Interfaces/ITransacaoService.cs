using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Domain.Service.Interfaces
{
    public interface ITransacaoService
    {
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
    }
}
