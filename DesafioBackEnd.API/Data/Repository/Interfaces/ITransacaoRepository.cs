using DesafioBackEnd.API.Application.Dto.Transacoes;
using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Data.Repository.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
        Task<Transacao> GetByIdAsync(long? id);
        Task<IEnumerable<DetailTransacaoDto>> GetTransacoesAsync(int pageNumber, int pageSize);
    }
}
