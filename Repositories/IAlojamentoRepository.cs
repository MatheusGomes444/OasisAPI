using OasisApi.Models;

namespace OasisApi.Repositories
{
    public interface IAlojamentoRepository
    {
        Task<List<Alojamento>> GetAllWithMoradoresAsync();
        Task<Alojamento?> GetByUuidAsync(Guid uuid);
        Task<Alojamento?> GetByUuidWithMoradoresAsync(Guid uuid);
        Task AddAsync(Alojamento alojamento);
        Task UpdateAsync(Alojamento alojamento);
        Task DeleteAsync(Alojamento alojamento);

        Task AddFilaDeEsperaAsync(FilaDeEspera filaDeEspera);

        // Chave interna (int) - nunca exposta pela API.
        Task<List<FilaDeEspera>> GetFilaDeEsperaAsync(int alojamentoId);
    }
}
