namespace OasisApi.Application.Interfaces
{
    // Abstrai a transação do banco para fora da Application, que não deve conhecer EF Core.
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
