namespace OasisApi.Dtos.Alojamentos
{
    public class FilaDeEsperaResponseDto
    {
        public Guid Id { get; set; }
        public Guid MoradorId { get; set; }
        public string? MoradorNome { get; set; }
        public Guid AlojamentoId { get; set; }
        public DateTime DataEntrada { get; set; }
    }
}
