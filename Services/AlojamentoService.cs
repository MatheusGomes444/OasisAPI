using Microsoft.EntityFrameworkCore.Storage;
using OasisApi.Common.Exceptions;
using OasisApi.Common.Mapping;
using OasisApi.Data;
using OasisApi.Dtos.Alojamentos;
using OasisApi.Dtos.Moradores;
using OasisApi.Models;
using OasisApi.Repositories;

namespace OasisApi.Services
{
    public class AlojamentoService : IAlojamentoService
    {
        private readonly IAlojamentoRepository _alojamentoRepository;
        private readonly IMoradorRepository _moradorRepository;
        // Injetado diretamente só para demarcar a transação em MudarAlojamentoOuFilaDeEsperaAsync,
        // que precisa que a leitura de capacidade e a escrita subsequente sejam atômicas.
        private readonly DataContext _context;

        public AlojamentoService(IAlojamentoRepository alojamentoRepository, IMoradorRepository moradorRepository, DataContext context)
        {
            _alojamentoRepository = alojamentoRepository;
            _moradorRepository = moradorRepository;
            _context = context;
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
            var alojamento = dto.ToEntity();
            await _alojamentoRepository.AddAsync(alojamento);
            return alojamento.ToResponseDto();
        }

        public async Task<AlojamentoResponseDto> UpdateAsync(Guid id, AlojamentoUpdateDto dto)
        {
            var alojamento = await _alojamentoRepository.GetByUuidAsync(id)
                ?? throw new NotFoundException($"Alojamento com ID {id} não encontrado.");

            dto.ApplyTo(alojamento);
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

            var morador = dto.ToEntity();
            morador.AlojamentoId = alojamento.Id;
            morador.Alojamento = alojamento;
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
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

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
                    morador.AlojamentoId = novoAlojamento.Id;
                    morador.Alojamento = novoAlojamento;
                    await _moradorRepository.UpdateAsync(morador);
                    message = "Morador movido para o novo alojamento com sucesso.";
                }

                await transaction.CommitAsync();
                return message;
            }
            catch
            {
                await transaction.RollbackAsync();
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
    }
}
