using OasisApi.Dtos.Moradores;

namespace OasisApi.Services
{
    public interface IMoradorService
    {
        Task<List<MoradorResponseDto>> GetAllAsync();
        Task<MoradorResponseDto> GetByIdAsync(int id);
        Task<MoradorResponseDto> CreateAsync(MoradorCreateDto dto);
        Task<MoradorResponseDto> UpdateAsync(int id, MoradorUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
