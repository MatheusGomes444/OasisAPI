using OasisApi.Dtos.Alojamentos;
using OasisApi.Dtos.Moradores;

namespace OasisApi.Services
{
    public interface IAlojamentoService
    {
        Task<List<AlojamentoListItemDto>> GetAllAsync();
        Task<AlojamentoResponseDto> GetByIdAsync(int id);
        Task<AlojamentoResponseDto> CreateAsync(AlojamentoCreateDto dto);
        Task<AlojamentoResponseDto> UpdateAsync(int id, AlojamentoUpdateDto dto);
        Task DeleteAsync(int id);

        Task<MoradorResponseDto> AdicionarMoradorAsync(int alojamentoId, MoradorCreateDto dto);
        Task<List<MoradorResponseDto>> ListarMoradoresAsync(int alojamentoId);
        Task<string> MudarAlojamentoOuFilaDeEsperaAsync(MudancaAlojamentoRequestDto dto);
        Task<List<FilaDeEsperaResponseDto>> ListarFilaDeEsperaAsync(int alojamentoId);
    }
}
