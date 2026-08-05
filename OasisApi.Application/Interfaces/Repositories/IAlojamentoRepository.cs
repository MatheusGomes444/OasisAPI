using OasisApi.Domain.Entities;

namespace OasisApi.Application.Interfaces.Repositories
{
    public interface IAlojamentoRepository
    {
        Task<(List<Alojamento> Items, int TotalCount)> GetPagedAsync(string? nome, int page, int pageSize);
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
