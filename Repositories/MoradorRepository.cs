using Microsoft.EntityFrameworkCore;
using OasisApi.Data;
using OasisApi.Models;

namespace OasisApi.Repositories
{
    public class MoradorRepository : IMoradorRepository
    {
        private readonly DataContext _context;

        public MoradorRepository(DataContext context)
        {
            _context = context;
        }

        public Task<List<Morador>> GetAllAsync() =>
            _context.Moradores.ToListAsync();

        public Task<Morador?> GetByIdAsync(int id) =>
            _context.Moradores.FirstOrDefaultAsync(m => m.Id == id);

        public Task<Morador?> GetByIdWithAlojamentoAsync(int id) =>
            _context.Moradores.Include(m => m.Alojamento).FirstOrDefaultAsync(m => m.Id == id);

        public Task<List<Morador>> GetByAlojamentoIdAsync(int alojamentoId) =>
            _context.Moradores.Where(m => m.AlojamentoId == alojamentoId).ToListAsync();

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
