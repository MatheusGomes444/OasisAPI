namespace OasisApi.Domain.Entities
{
    public class Alojamento
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; } = UuidV7.NewGuid();
        public string Nome { get; set; } = string.Empty;
         public string Equipe { get; set; } = string.Empty;
        public int Telefone { get; set; }
        public string Email { get; set; } = string.Empty;
        public int CapacidadeMaxima { get; set; }
        public string Pet { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Pertences { get; set; } = string.Empty;
        public string Refeicoes {get; set; } = string.Empty;
        

        // Relacionamento 1:N com Moradores
        public ICollection<Morador> Moradores { get; set; } = new List<Morador>();

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
}
