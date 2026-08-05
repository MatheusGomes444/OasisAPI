namespace OasisApi.Domain.Entities;

public class Morador
{
    public int Id { get; set; }
    public Guid Uuid { get; set; } = UuidV7.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string RG { get; set; } = string.Empty;
    public int Telefone { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public int Idade { get; set; }
    public int Datanascimento { get; set; }
    public string Nacionalidade { get; set; } = string.Empty;
    public string Observacoes { get; set; } = string.Empty;
    public bool Ativo { get; set; }

    // Relacionamento com Alojamento (chave interna, nunca exposta pela API)
    public int AlojamentoId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Alojamento? Alojamento { get; set; }

    // Trilha de auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int? CreatedByUserId { get; set; }
    public int? UpdatedByUserId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public User? CreatedByUser { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public User? UpdatedByUser { get; set; }
}
