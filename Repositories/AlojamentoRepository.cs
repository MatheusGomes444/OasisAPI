using Microsoft.EntityFrameworkCore;
using OasisApi.Data;
using OasisApi.Models;

namespace OasisApi.Repositories
{
    public class AlojamentoRepository : IAlojamentoRepository
    {
        private readonly DataContext _context;

        public AlojamentoRepository(DataContext context)
        {
            _context = context;
        }

        public Task<List<Alojamento>> GetAllWithMoradoresAsync() =>
            _context.Alojamentos.Include(a => a.Moradores).ToListAsync();

        public Task<Alojamento?> GetByIdAsync(int id) =>
            _context.Alojamentos.FirstOrDefaultAsync(a => a.Id == id);

        public Task<Alojamento?> GetByIdWithMoradoresAsync(int id) =>
            _context.Alojamentos.Include(a => a.Moradores).FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAsync(Alojamento alojamento)
        {
            _context.Alojamentos.Add(alojamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Alojamento alojamento)
        {
            _context.Alojamentos.Update(alojamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Alojamento alojamento)
        {
            _context.Alojamentos.Remove(alojamento);
            await _context.SaveChangesAsync();
        }

        public async Task AddFilaDeEsperaAsync(FilaDeEspera filaDeEspera)
        {
            _context.FilasDeEspera.Add(filaDeEspera);
            await _context.SaveChangesAsync();
        }

        public Task<List<FilaDeEspera>> GetFilaDeEsperaAsync(int alojamentoId) =>
            _context.FilasDeEspera
                .Where(f => f.AlojamentoId == alojamentoId)
                .OrderBy(f => f.DataEntrada)
                .Include(f => f.Morador)
                .ToListAsync();
    }
}
