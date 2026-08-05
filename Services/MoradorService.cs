using OasisApi.Common.Exceptions;
using OasisApi.Common.Mapping;
using OasisApi.Dtos.Moradores;
using OasisApi.Repositories;

namespace OasisApi.Services
{
    public class MoradorService : IMoradorService
    {
        private readonly IMoradorRepository _moradorRepository;

        public MoradorService(IMoradorRepository moradorRepository)
        {
            _moradorRepository = moradorRepository;
        }

        public async Task<List<MoradorResponseDto>> GetAllAsync()
        {
            var moradores = await _moradorRepository.GetAllAsync();
            return moradores.Select(m => m.ToResponseDto()).ToList();
        }

        public async Task<MoradorResponseDto> GetByIdAsync(int id)
        {
            var morador = await _moradorRepository.GetByIdWithAlojamentoAsync(id)
                ?? throw new NotFoundException($"Morador com ID {id} não encontrado.");

            return morador.ToResponseDto();
        }

        public async Task<MoradorResponseDto> CreateAsync(MoradorCreateDto dto)
        {
            var morador = dto.ToEntity();
            await _moradorRepository.AddAsync(morador);
            return morador.ToResponseDto();
        }

        public async Task<MoradorResponseDto> UpdateAsync(int id, MoradorUpdateDto dto)
        {
            var morador = await _moradorRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Morador com ID {id} não encontrado.");

            dto.ApplyTo(morador);
            await _moradorRepository.UpdateAsync(morador);
            return morador.ToResponseDto();
        }

        public async Task DeleteAsync(int id)
        {
            var morador = await _moradorRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Morador com ID {id} não encontrado.");

            await _moradorRepository.DeleteAsync(morador);
        }
    }
}
