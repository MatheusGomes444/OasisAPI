using OasisApi.Application.Common;
using OasisApi.Application.Dtos.Common;
using OasisApi.Application.Dtos.Moradores;
using OasisApi.Application.Exceptions;
using OasisApi.Application.Interfaces;
using OasisApi.Application.Interfaces.Repositories;
using OasisApi.Application.Mapping;
using OasisApi.Domain.Entities;

namespace OasisApi.Application.Services
{
    public class MoradorService : IMoradorService
    {
        private readonly IMoradorRepository _moradorRepository;
        private readonly IAlojamentoRepository _alojamentoRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public MoradorService(
            IMoradorRepository moradorRepository,
            IAlojamentoRepository alojamentoRepository,
            IUserRepository userRepository,
            ICurrentUserService currentUserService)
        {
            _moradorRepository = moradorRepository;
            _alojamentoRepository = alojamentoRepository;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResultDto<MoradorResponseDto>> GetAllAsync(MoradorQueryDto query)
        {
            var (page, pageSize) = Paging.Normalize(query.Page, query.PageSize);
            var (items, totalCount) = await _moradorRepository.GetPagedAsync(query.Nome, query.Ativo, page, pageSize);

            return new PagedResultDto<MoradorResponseDto>
            {
                Items = items.Select(m => m.ToResponseDto()).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<MoradorResponseDto> GetByIdAsync(Guid id)
        {
            var morador = await _moradorRepository.GetByUuidWithAlojamentoAsync(id)
                ?? throw new NotFoundException($"Morador com ID {id} não encontrado.");

            return morador.ToResponseDto();
        }

        public async Task<MoradorResponseDto> CreateAsync(MoradorCreateDto dto)
        {
            var alojamento = await _alojamentoRepository.GetByUuidAsync(dto.AlojamentoId)
                ?? throw new NotFoundException($"Alojamento com ID {dto.AlojamentoId} não encontrado.");

            var currentUser = await GetCurrentUserAsync();

            var morador = dto.ToEntity();
            morador.AlojamentoId = alojamento.Id;
            morador.Alojamento = alojamento;
            morador.CreatedAt = DateTime.UtcNow;
            morador.CreatedByUserId = currentUser?.Id;
            morador.CreatedByUser = currentUser;

            await _moradorRepository.AddAsync(morador);
            return morador.ToResponseDto();
        }

        public async Task<MoradorResponseDto> UpdateAsync(Guid id, MoradorUpdateDto dto)
        {
            var morador = await _moradorRepository.GetByUuidWithAlojamentoAsync(id)
                ?? throw new NotFoundException($"Morador com ID {id} não encontrado.");

            var currentUser = await GetCurrentUserAsync();

            dto.ApplyTo(morador);
            morador.UpdatedAt = DateTime.UtcNow;
            morador.UpdatedByUserId = currentUser?.Id;
            morador.UpdatedByUser = currentUser;

            await _moradorRepository.UpdateAsync(morador);
            return morador.ToResponseDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            var morador = await _moradorRepository.GetByUuidAsync(id)
                ?? throw new NotFoundException($"Morador com ID {id} não encontrado.");

            await _moradorRepository.DeleteAsync(morador);
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var uuid = _currentUserService.UserUuid;
            return uuid.HasValue ? await _userRepository.GetByUuidAsync(uuid.Value) : null;
        }
    }
}
