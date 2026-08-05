using OasisApi.Application.Dtos.Moradores;
using OasisApi.Domain.Entities;

namespace OasisApi.Application.Mapping
{
    public static class MoradorMappingExtensions
    {
        public static MoradorResponseDto ToResponseDto(this Morador morador)
        {
            return new MoradorResponseDto
            {
                Id = morador.Uuid,
                Nome = morador.Nome,
                CPF = morador.CPF,
                RG = morador.RG,
                Telefone = morador.Telefone,
                Endereco = morador.Endereco,
                Idade = morador.Idade,
                Datanascimento = morador.Datanascimento,
                Nacionalidade = morador.Nacionalidade,
                Observacoes = morador.Observacoes,
                Ativo = morador.Ativo,
                AlojamentoId = morador.Alojamento?.Uuid ?? Guid.Empty,
                AlojamentoNome = morador.Alojamento?.Nome,
                CreatedAt = morador.CreatedAt,
                UpdatedAt = morador.UpdatedAt,
                CreatedByEmail = morador.CreatedByUser?.Email,
                UpdatedByEmail = morador.UpdatedByUser?.Email
            };
        }

        // AlojamentoId (int) precisa ser resolvido a partir do Uuid do alojamento
        // pelo chamador (AlojamentoService/MoradorService) antes de persistir.
        public static Morador ToEntity(this MoradorCreateDto dto)
        {
            return new Morador
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                RG = dto.RG,
                Telefone = dto.Telefone,
                Endereco = dto.Endereco,
                Idade = dto.Idade,
                Datanascimento = dto.Datanascimento,
                Nacionalidade = dto.Nacionalidade,
                Observacoes = dto.Observacoes,
                Ativo = dto.Ativo
            };
        }

        public static void ApplyTo(this MoradorUpdateDto dto, Morador morador)
        {
            morador.Nome = dto.Nome;
            morador.CPF = dto.CPF;
            morador.RG = dto.RG;
            morador.Endereco = dto.Endereco;
            morador.Idade = dto.Idade;
            morador.Observacoes = dto.Observacoes;
            morador.Ativo = dto.Ativo;
        }
    }
}
