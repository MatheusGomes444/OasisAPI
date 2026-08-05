using OasisApi.Models;

namespace OasisApi.Repositories
{
    public interface IMoradorRepository
    {
        Task<List<Morador>> GetAllAsync();
        Task<Morador?> GetByIdAsync(Guid id);
        Task<Morador?> GetByIdWithAlojamentoAsync(Guid id);
        Task<List<Morador>> GetByAlojamentoIdAsync(Guid alojamentoId);
        Task<int> CountByAlojamentoIdAsync(Guid alojamentoId);
        Task AddAsync(Morador morador);
        Task UpdateAsync(Morador morador);
        Task DeleteAsync(Morador morador);
    }
}
