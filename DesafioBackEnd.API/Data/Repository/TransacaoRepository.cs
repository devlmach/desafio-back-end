using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.API.Data.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransacaoRepository(ApplicationDbContext dbContext, IUsuarioService usuarioService)
        {
            _dbContext = dbContext;
        }

        public async Task<Transacao> CreateTransacaoAsync(Transacao transacao)
        {
            _dbContext.Add(transacao);
            await _dbContext.SaveChangesAsync();
            return transacao;
        }

        public async Task<Transacao> GetByIdAsync(long? id)
        {
            return await _dbContext.Transacoes.FindAsync(id);
        }

        public async Task<IEnumerable<Transacao>> GetTransacoesAsync()
        {
            return await _dbContext.Transacoes.ToListAsync();
        }
    }
}
