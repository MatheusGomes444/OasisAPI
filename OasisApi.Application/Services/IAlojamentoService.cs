using OasisApi.Application.Dtos.Alojamentos;
using OasisApi.Application.Dtos.Common;
using OasisApi.Application.Dtos.Moradores;

namespace OasisApi.Application.Services
{
    public interface IAlojamentoService
    {
        Task<PagedResultDto<AlojamentoListItemDto>> GetAllAsync(AlojamentoQueryDto query);
        Task<AlojamentoResponseDto> GetByIdAsync(Guid id);
        Task<AlojamentoResponseDto> CreateAsync(AlojamentoCreateDto dto);
        Task<AlojamentoResponseDto> UpdateAsync(Guid id, AlojamentoUpdateDto dto);
        Task DeleteAsync(Guid id);

        Task<MoradorResponseDto> AdicionarMoradorAsync(Guid alojamentoId, MoradorCreateDto dto);
        Task<List<MoradorResponseDto>> ListarMoradoresAsync(Guid alojamentoId);
        Task<string> MudarAlojamentoOuFilaDeEsperaAsync(MudancaAlojamentoRequestDto dto);
        Task<List<FilaDeEsperaResponseDto>> ListarFilaDeEsperaAsync(Guid alojamentoId);
    }
}
