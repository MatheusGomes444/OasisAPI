using OasisApi.Common.Mapping;
using OasisApi.Dtos.Alojamentos;
using OasisApi.Models;

namespace OasisApi.Common.Mapping
{
    public static class AlojamentoMappingExtensions
    {
        public static AlojamentoResponseDto ToResponseDto(this Alojamento alojamento)
        {
            return new AlojamentoResponseDto
            {
                Id = alojamento.Id,
                Nome = alojamento.Nome,
                Equipe = alojamento.Equipe,
                Telefone = alojamento.Telefone,
                Email = alojamento.Email,
                CapacidadeMaxima = alojamento.CapacidadeMaxima,
                Pet = alojamento.Pet,
                Sexo = alojamento.Sexo,
                Pertences = alojamento.Pertences,
                Refeicoes = alojamento.Refeicoes,
                Moradores = alojamento.Moradores.Select(m => m.ToResponseDto()).ToList()
            };
        }

        public static AlojamentoListItemDto ToListItemDto(this Alojamento alojamento)
        {
            return new AlojamentoListItemDto
            {
                Id = alojamento.Id,
                Nome = alojamento.Nome,
                CapacidadeMaxima = alojamento.CapacidadeMaxima,
                QuantidadeMoradores = alojamento.Moradores.Count
            };
        }

        public static Alojamento ToEntity(this AlojamentoCreateDto dto)
        {
            return new Alojamento
            {
                Nome = dto.Nome,
                Equipe = dto.Equipe,
                Telefone = dto.Telefone,
                Email = dto.Email,
                CapacidadeMaxima = dto.CapacidadeMaxima,
                Pet = dto.Pet,
                Sexo = dto.Sexo,
                Pertences = dto.Pertences,
                Refeicoes = dto.Refeicoes
            };
        }

        public static void ApplyTo(this AlojamentoUpdateDto dto, Alojamento alojamento)
        {
            alojamento.Nome = dto.Nome;
            alojamento.Equipe = dto.Equipe;
            alojamento.Telefone = dto.Telefone;
            alojamento.Email = dto.Email;
            alojamento.CapacidadeMaxima = dto.CapacidadeMaxima;
            alojamento.Pet = dto.Pet;
            alojamento.Sexo = dto.Sexo;
            alojamento.Pertences = dto.Pertences;
            alojamento.Refeicoes = dto.Refeicoes;
        }

        public static FilaDeEsperaResponseDto ToResponseDto(this FilaDeEspera filaDeEspera)
        {
            return new FilaDeEsperaResponseDto
            {
                Id = filaDeEspera.Id,
                MoradorId = filaDeEspera.MoradorId,
                MoradorNome = filaDeEspera.Morador?.Nome,
                AlojamentoId = filaDeEspera.AlojamentoId,
                DataEntrada = filaDeEspera.DataEntrada
            };
        }
    }
}
