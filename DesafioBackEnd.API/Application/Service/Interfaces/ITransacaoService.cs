using DesafioBackEnd.API.Application.Dto.Transacao;

namespace DesafioBackEnd.API.Application.Service.Interfaces
{
    public interface ITransacaoService
    {
        Task<IEnumerable<DetailTransacaoDto>> GetTransacoesAsync();
        Task<DetailTransacaoDto> GetTransacaoByIdAsync(long? id);
        Task CreateTransacaoAsync(CreateTransacaoDto createTransacaoDto);
    }
}
