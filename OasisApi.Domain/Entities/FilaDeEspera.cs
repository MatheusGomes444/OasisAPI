namespace OasisApi.Domain.Entities;
public class FilaDeEspera
{
    public int Id { get; set; }
    public Guid Uuid { get; set; } = Guid.NewGuid();
    public int MoradorId { get; set; }
    public int AlojamentoId { get; set; }
    public DateTime DataEntrada { get; set; }

    // Relacionamentos
    public Morador? Morador { get; set; }
    public Alojamento? Alojamento { get; set; }
}
