using OasisApi.Models;

namespace OasisApi.Repositories
{
    public interface IMoradorRepository
    {
        Task<List<Morador>> GetAllAsync();
        Task<Morador?> GetByIdAsync(int id);
        Task<Morador?> GetByIdWithAlojamentoAsync(int id);
        Task<List<Morador>> GetByAlojamentoIdAsync(int alojamentoId);
        Task<int> CountByAlojamentoIdAsync(int alojamentoId);
        Task AddAsync(Morador morador);
        Task UpdateAsync(Morador morador);
        Task DeleteAsync(Morador morador);
    }
}
