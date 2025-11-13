using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Domain.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetByIdAsync(int? id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int? id);
    }
}
