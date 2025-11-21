using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Data.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<DetailUsuarioDto>> GetUsuariosAsync(string? nomeCompleto, string? cpf, string? email,
            UserType? tipo, bool? isActive, int pageNumber, int pageSize);
        Task<Usuario> GetByIdAsync(long? id);
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task<Usuario> DeleteAsync(Usuario usuario);
        //Task<IEnumerable<Usuario>> GetUsuariosAsync();
    }
}
