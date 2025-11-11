using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetByIdAsync(int? id);
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task<Usuario> DeleteAsync(Usuario usuario);
    }
}
