using OasisApi.Application.Dtos.Alojamentos;
using OasisApi.Application.Dtos.Moradores;
using OasisApi.Application.Exceptions;
using OasisApi.Application.Interfaces;
using OasisApi.Application.Interfaces.Repositories;
using OasisApi.Application.Mapping;
using OasisApi.Domain.Entities;

namespace OasisApi.Application.Services
{
    public class AlojamentoService : IAlojamentoService
    {
        private readonly IAlojamentoRepository _alojamentoRepository;
        private readonly IMoradorRepository _moradorRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public AlojamentoService(
            IAlojamentoRepository alojamentoRepository,
            IMoradorRepository moradorRepository,
            IUserRepository userRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _alojamentoRepository = alojamentoRepository;
            _moradorRepository = moradorRepository;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AlojamentoListItemDto>> GetAllAsync()
        {
            var alojamentos = await _alojamentoRepository.GetAllWithMoradoresAsync();
            return alojamentos.Select(a => a.ToListItemDto()).ToList();
        }

        public async Task<AlojamentoResponseDto> GetByIdAsync(Guid id)
        {
            var alojamento = await _alojamentoRepository.GetByUuidWithMoradoresAsync(id)
                ?? throw new NotFoundException($"Alojamento com ID {id} não encontrado.");

            return alojamento.ToResponseDto();
        }

        public async Task<AlojamentoResponseDto> CreateAsync(AlojamentoCreateDto dto)
        {
            var currentUser = await GetCurrentUserAsync();

            var alojamento = dto.ToEntity();
            alojamento.CreatedAt = DateTime.UtcNow;
            alojamento.CreatedByUserId = currentUser?.Id;
            alojamento.CreatedByUser = currentUser;

            await _alojamentoRepository.AddAsync(alojamento);
            return alojamento.ToResponseDto();
        }

        public async Task<AlojamentoResponseDto> UpdateAsync(Guid id, AlojamentoUpdateDto dto)
        {
            var alojamento = await _alojamentoRepository.GetByUuidWithMoradoresAsync(id)
                ?? throw new NotFoundException($"Alojamento com ID {id} não encontrado.");

            var currentUser = await GetCurrentUserAsync();

            dto.ApplyTo(alojamento);
            alojamento.UpdatedAt = DateTime.UtcNow;
            alojamento.UpdatedByUserId = currentUser?.Id;
            alojamento.UpdatedByUser = currentUser;

            await _alojamentoRepository.UpdateAsync(alojamento);
            return alojamento.ToResponseDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            var alojamento = await _alojamentoRepository.GetByUuidAsync(id)
                ?? throw new NotFoundException($"Alojamento com ID {id} não encontrado.");

            await _alojamentoRepository.DeleteAsync(alojamento);
        }

        public async Task<MoradorResponseDto> AdicionarMoradorAsync(Guid alojamentoId, MoradorCreateDto dto)
        {
            var alojamento = await _alojamentoRepository.GetByUuidWithMoradoresAsync(alojamentoId)
                ?? throw new NotFoundException($"Alojamento com ID {alojamentoId} não encontrado.");

            if (alojamento.Moradores.Count >= alojamento.CapacidadeMaxima)
            {
                throw new BadRequestException($"Alojamento com ID {alojamentoId} já atingiu sua capacidade máxima.");
            }

            if (!dto.Ativo)
            {
                throw new BadRequestException("Apenas moradores ativos podem ser alocados.");
            }

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

        public async Task<List<MoradorResponseDto>> ListarMoradoresAsync(Guid alojamentoId)
        {
            var alojamento = await _alojamentoRepository.GetByUuidWithMoradoresAsync(alojamentoId)
                ?? throw new NotFoundException($"Alojamento com ID {alojamentoId} não encontrado.");

            return alojamento.Moradores.Select(m => m.ToResponseDto()).ToList();
        }

        public async Task<string> MudarAlojamentoOuFilaDeEsperaAsync(MudancaAlojamentoRequestDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var morador = await _moradorRepository.GetByUuidWithAlojamentoAsync(dto.MoradorId)
                    ?? throw new NotFoundException("Morador não encontrado.");

                var novoAlojamento = await _alojamentoRepository.GetByUuidAsync(dto.NovoAlojamentoId)
                    ?? throw new NotFoundException("Alojamento não encontrado.");

                var moradoresNoAlojamento = await _moradorRepository.CountByAlojamentoIdAsync(novoAlojamento.Id);

                string message;
                if (moradoresNoAlojamento >= novoAlojamento.CapacidadeMaxima)
                {
                    await _alojamentoRepository.AddFilaDeEsperaAsync(new FilaDeEspera
                    {
                        MoradorId = morador.Id,
                        AlojamentoId = novoAlojamento.Id
                    });
                    message = "Morador colocado na fila de espera, pois o alojamento está cheio.";
                }
                else
                {
                    var currentUser = await GetCurrentUserAsync();

                    morador.AlojamentoId = novoAlojamento.Id;
                    morador.Alojamento = novoAlojamento;
                    morador.UpdatedAt = DateTime.UtcNow;
                    morador.UpdatedByUserId = currentUser?.Id;
                    morador.UpdatedByUser = currentUser;
                    await _moradorRepository.UpdateAsync(morador);
                    message = "Morador movido para o novo alojamento com sucesso.";
                }

                await _unitOfWork.CommitTransactionAsync();
                return message;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<List<FilaDeEsperaResponseDto>> ListarFilaDeEsperaAsync(Guid alojamentoId)
        {
            var alojamento = await _alojamentoRepository.GetByUuidAsync(alojamentoId)
                ?? throw new NotFoundException($"Alojamento com ID {alojamentoId} não encontrado.");

            var filaDeEspera = await _alojamentoRepository.GetFilaDeEsperaAsync(alojamento.Id);
            return filaDeEspera.Select(f => f.ToResponseDto()).ToList();
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var uuid = _currentUserService.UserUuid;
            return uuid.HasValue ? await _userRepository.GetByUuidAsync(uuid.Value) : null;
        }
    }
}
