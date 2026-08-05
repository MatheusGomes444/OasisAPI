namespace OasisApi.Application.Dtos.Alojamentos
{
    public class AlojamentoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Equipe { get; set; } = string.Empty;
        public int Telefone { get; set; }
        public string Email { get; set; } = string.Empty;
        public int CapacidadeMaxima { get; set; }
        public string Pet { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Pertences { get; set; } = string.Empty;
        public string Refeicoes { get; set; } = string.Empty;
    }
}
