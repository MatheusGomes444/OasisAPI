namespace OasisApi.Application.Interfaces
{
    // Abstrai a leitura do usuário autenticado da requisição atual, sem a Application
    // precisar conhecer HttpContext/ASP.NET Core.
    public interface ICurrentUserService
    {
        Guid? UserUuid { get; }
    }
}
