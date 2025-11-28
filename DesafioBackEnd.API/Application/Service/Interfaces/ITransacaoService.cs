using DesafioBackEnd.API.Application.Dto.Transacoes;

namespace DesafioBackEnd.API.Application.Service.Interfaces
{
    public interface ITransacaoService
    {
        Task<IEnumerable<DetailTransacaoDto>> GetTransacoesAsync(int pageNumber, int pageSize);
        Task<IEnumerable<DetailTransacaoDto>> GetTransacoesUserAsync(long id, int pageNumber, int pageSize);
        Task<DetailTransacaoDto> GetTransacaoByIdAsync(long? id);
        Task CreateTransacaoAsync(CreateTransacaoDto createTransacaoDto);
    }
}
