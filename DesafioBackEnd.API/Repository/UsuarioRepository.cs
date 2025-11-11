using DesafioBackEnd.API.Context;
using DesafioBackEnd.API.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            try
            {
                _dbContext.Add(usuario);
                await _dbContext.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? "";

                if (errorMessage.Contains("Cpf"))
                    throw new Exception("Erro: Cpf já está em uso.");

                if(errorMessage.Contains("Email"))
                    throw new Exception("Email já está em uso.");

                throw;
            }
        }

        public async Task<Usuario> DeleteAsync(Usuario usuario)
        {
            _dbContext.Remove(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> GetByIdAsync(int? id) => await _dbContext.Usuarios.FindAsync(id);
        
        public async Task<IEnumerable<Usuario>> GetUsuariosAsync() => await _dbContext.Usuarios.ToListAsync();

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _dbContext.Update(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
    }
}
