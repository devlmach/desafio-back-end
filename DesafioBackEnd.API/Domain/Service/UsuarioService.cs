using DesafioBackEnd.API.Domain.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Service.Interfaces;
using DesafioBackEnd.API.Entity;

namespace DesafioBackEnd.API.Domain.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task DeleteAsync(int? id)
        {
            var usuarioById = _usuarioRepository.GetByIdAsync(id).Result;
            await _usuarioRepository.DeleteAsync(usuarioById);

        }

        public async Task<Usuario> GetByIdAsync(int? id) => await _usuarioRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync() => await _usuarioRepository.GetUsuariosAsync();

        public async Task UpdateAsync(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }
    }
}
