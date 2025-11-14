using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Service.Interfaces
{
    public interface ITransacaoService
    {
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
    }
}
