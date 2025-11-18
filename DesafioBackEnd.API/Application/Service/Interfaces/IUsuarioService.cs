using DesafioBackEnd.API.Application.Dto.Usuarios;

namespace DesafioBackEnd.API.Application.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<DetailUsuarioDto>> GetUsuariosAsync();
        Task<DetailUsuarioDto> GetByIdAsync(long? id);
        Task AddAsync(CreateUsuarioDto createUsuarioDto);
        Task UpdateAsync(UpdateUsuarioDto updateUsuarioDto);
        Task DeleteAsync(long? id);
    }
}
