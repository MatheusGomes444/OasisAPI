using OasisApi.Application.Dtos.Common;
using OasisApi.Application.Dtos.Moradores;

namespace OasisApi.Application.Services
{
    public interface IMoradorService
    {
        Task<PagedResultDto<MoradorResponseDto>> GetAllAsync(MoradorQueryDto query);
        Task<MoradorResponseDto> GetByIdAsync(Guid id);
        Task<MoradorResponseDto> CreateAsync(MoradorCreateDto dto);
        Task<MoradorResponseDto> UpdateAsync(Guid id, MoradorUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
