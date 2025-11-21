using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<DetailUsuarioDto>> GetUsuariosAsync(string? nomeCompleto, string? cpf, string? email, UserType? tipo, bool? isActive, int pageNumber, int pageSize);
        Task<DetailUsuarioDto> GetByIdAsync(long? id);
        Task AddAsync(CreateUsuarioDto createUsuarioDto);
        Task UpdateAsync(UpdateUsuarioDto updateUsuarioDto);
        Task DeleteAsync(long? id);
    }
}
