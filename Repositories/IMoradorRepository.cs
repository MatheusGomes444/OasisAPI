using OasisApi.Models;

namespace OasisApi.Repositories
{
    public interface IMoradorRepository
    {
        Task<List<Morador>> GetAllAsync();
        Task<Morador?> GetByUuidAsync(Guid uuid);
        Task<Morador?> GetByUuidWithAlojamentoAsync(Guid uuid);

        // Chaves internas (int) - nunca expostas pela API, usadas só para relacionamentos.
        Task<int> CountByAlojamentoIdAsync(int alojamentoId);

        Task AddAsync(Morador morador);
        Task UpdateAsync(Morador morador);
        Task DeleteAsync(Morador morador);
    }
}
