using OasisApi.Models;

namespace OasisApi.Repositories
{
    public interface IAlojamentoRepository
    {
        Task<List<Alojamento>> GetAllWithMoradoresAsync();
        Task<Alojamento?> GetByIdAsync(Guid id);
        Task<Alojamento?> GetByIdWithMoradoresAsync(Guid id);
        Task AddAsync(Alojamento alojamento);
        Task UpdateAsync(Alojamento alojamento);
        Task DeleteAsync(Alojamento alojamento);

        Task AddFilaDeEsperaAsync(FilaDeEspera filaDeEspera);
        Task<List<FilaDeEspera>> GetFilaDeEsperaAsync(Guid alojamentoId);
    }
}
