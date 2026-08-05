using Microsoft.EntityFrameworkCore;
using OasisApi.Application.Interfaces.Repositories;
using OasisApi.Domain.Entities;
using OasisApi.Infrastructure.Persistence;

namespace OasisApi.Infrastructure.Repositories
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

        public Task<Alojamento?> GetByUuidAsync(Guid uuid) =>
            _context.Alojamentos.FirstOrDefaultAsync(a => a.Uuid == uuid);

        public Task<Alojamento?> GetByUuidWithMoradoresAsync(Guid uuid) =>
            _context.Alojamentos.Include(a => a.Moradores).FirstOrDefaultAsync(a => a.Uuid == uuid);

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
                .Include(f => f.Alojamento)
                .ToListAsync();
    }
}
