using Microsoft.EntityFrameworkCore;
using OasisApi.Application.Interfaces.Repositories;
using OasisApi.Domain.Entities;
using OasisApi.Infrastructure.Persistence;

namespace OasisApi.Infrastructure.Repositories
{
    public class MoradorRepository : IMoradorRepository
    {
        private readonly DataContext _context;

        public MoradorRepository(DataContext context)
        {
            _context = context;
        }

        public Task<List<Morador>> GetAllAsync() =>
            _context.Moradores
                .Include(m => m.Alojamento)
                .Include(m => m.CreatedByUser)
                .Include(m => m.UpdatedByUser)
                .ToListAsync();

        public Task<Morador?> GetByUuidAsync(Guid uuid) =>
            _context.Moradores.FirstOrDefaultAsync(m => m.Uuid == uuid);

        public Task<Morador?> GetByUuidWithAlojamentoAsync(Guid uuid) =>
            _context.Moradores
                .Include(m => m.Alojamento)
                .Include(m => m.CreatedByUser)
                .Include(m => m.UpdatedByUser)
                .FirstOrDefaultAsync(m => m.Uuid == uuid);

        public Task<int> CountByAlojamentoIdAsync(int alojamentoId) =>
            _context.Moradores.CountAsync(m => m.AlojamentoId == alojamentoId);

        public async Task AddAsync(Morador morador)
        {
            _context.Moradores.Add(morador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Morador morador)
        {
            _context.Moradores.Update(morador);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Morador morador)
        {
            _context.Moradores.Remove(morador);
            await _context.SaveChangesAsync();
        }
    }
}
