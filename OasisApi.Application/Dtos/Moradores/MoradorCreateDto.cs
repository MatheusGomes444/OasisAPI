namespace OasisApi.Application.Dtos.Moradores
{
    public class MoradorCreateDto
    {
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
        public Guid AlojamentoId { get; set; }
    }
}
