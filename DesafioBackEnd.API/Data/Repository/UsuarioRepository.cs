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
                    throw new BadRequestException("Erro: Email já está em uso.");

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
        
        public async Task<IEnumerable<DetailUsuarioDto>> GetUsuariosAsync(string? nomeCompleto, string? cpf, string? email, UserType? tipo, UserRole? role, bool? isActive, int pageNumber, int pageSize)
        {
            var query = _dbContext.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nomeCompleto))
                query = query.Where(u => u.NomeCompleto.Contains(nomeCompleto));

            if (!string.IsNullOrWhiteSpace(cpf))
                query = query.Where(u => u.Cpf.Contains(cpf));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(u => u.Email.Contains(email));

            if (tipo.HasValue)
                query = query.Where(u => u.Tipo == tipo.Value);

            if (isActive.HasValue)
                query = query.Where(u => u.IsActive == isActive.Value);

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.Select(
                u => new DetailUsuarioDto
                {
                    Id = u.Id,
                    NomeCompleto = u.NomeCompleto,
                    Cpf = u.Cpf,
                    Email = u.Email,
                    Senha = u.Senha,
                    Tipo = u.Tipo,
                    Role = u.Role,
                    Carteira = u.Carteira,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    IsActive = u.IsActive,
                }).ToListAsync();
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _dbContext.Update(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
    }
}
