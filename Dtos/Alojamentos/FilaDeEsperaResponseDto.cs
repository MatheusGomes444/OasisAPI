namespace OasisApi.Dtos.Alojamentos
{
    public class FilaDeEsperaResponseDto
    {
        public int Id { get; set; }
        public int MoradorId { get; set; }
        public string? MoradorNome { get; set; }
        public int AlojamentoId { get; set; }
        public DateTime DataEntrada { get; set; }
    }
}
