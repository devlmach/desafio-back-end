using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.API.Data.Repository
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
                    throw new BadRequestException("Erro: Cpf já está em uso.");

                if(errorMessage.Contains("Email"))
                    throw new BadRequestException("Email já está em uso.");

                throw;
            }
        }

        public async Task<Usuario> DeleteAsync(Usuario usuario)
        {
            usuario.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> GetByIdAsync(long? id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }
        
        public async Task<IEnumerable<Usuario>> GetUsuariosAsync() => await _dbContext.Usuarios.ToListAsync();

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _dbContext.Update(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
    }
}
