using DesafioBackEnd.API.Application.Dto.Transacoes;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.API.Data.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransacaoRepository(ApplicationDbContext dbContext)
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

        public async Task<IEnumerable<Transacao>> GetByUserAsync(long userId, int pageNumber, int pageSize)
        {
            return await _dbContext.Transacoes
                .Where(t => t.IdSender == userId || t.IdReceiver == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<DetailTransacaoDto>> GetTransacoesAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Transacoes.AsQueryable();

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.Select(
                u => new DetailTransacaoDto
                {
                    Id = u.Id,
                    IdSender = u.IdSender,
                    IdReceiver = u.IdReceiver,
                    QuantiaTransferida = u.QuantiaTransferida,
                    CreatedAt = u.CreatedAt
                }).ToListAsync();
        }
    }
}

