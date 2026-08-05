namespace OasisApi.Application.Dtos.Alojamentos
{
    public class AlojamentoQueryDto
    {
        public string? Nome { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
