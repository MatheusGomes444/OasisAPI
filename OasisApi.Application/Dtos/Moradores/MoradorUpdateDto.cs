namespace OasisApi.Application.Dtos.Moradores
{
    public class MoradorUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
