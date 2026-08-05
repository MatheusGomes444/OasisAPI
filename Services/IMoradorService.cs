using OasisApi.Dtos.Moradores;

namespace OasisApi.Services
{
    public interface IMoradorService
    {
        Task<List<MoradorResponseDto>> GetAllAsync();
        Task<MoradorResponseDto> GetByIdAsync(Guid id);
        Task<MoradorResponseDto> CreateAsync(MoradorCreateDto dto);
        Task<MoradorResponseDto> UpdateAsync(Guid id, MoradorUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
