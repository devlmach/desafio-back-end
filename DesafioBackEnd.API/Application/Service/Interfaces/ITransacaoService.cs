using DesafioBackEnd.API.Application.Dto.Transacao;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Service.Interfaces
{
    public interface ITransacaoService
    {
        Task<IEnumerable<DetailTransacaoDto>> GetTransacoesAsync(int pageNumber, int pageSize);
        Task<DetailTransacaoDto> GetTransacaoByIdAsync(long? id);
        Task CreateTransacaoAsync(CreateTransacaoDto createTransacaoDto);
    }
}
