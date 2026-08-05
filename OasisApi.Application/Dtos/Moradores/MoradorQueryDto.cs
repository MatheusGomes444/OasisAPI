namespace OasisApi.Application.Dtos.Moradores
{
    public class MoradorQueryDto
    {
        public string? Nome { get; set; }
        public bool? Ativo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
