namespace OasisApi.Dtos.Alojamentos
{
    public class AlojamentoListItemDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CapacidadeMaxima { get; set; }
        public int QuantidadeMoradores { get; set; }
    }
}
