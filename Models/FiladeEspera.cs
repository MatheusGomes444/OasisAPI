namespace OasisApi.Models;
public class FilaDeEspera
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MoradorId { get; set; }
    public Guid AlojamentoId { get; set; }
    public DateTime DataEntrada { get; set; }

    // Relacionamentos
    public Morador? Morador { get; set; }
    public Alojamento? Alojamento { get; set; }
}
