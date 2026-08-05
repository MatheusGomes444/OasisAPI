namespace OasisApi.Dtos.Moradores
{
    public class MoradorResponseDto
    {
        public int Id { get; set; }
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
        public int AlojamentoId { get; set; }
        public string? AlojamentoNome { get; set; }
    }
}
