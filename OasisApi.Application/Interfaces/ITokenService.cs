using OasisApi.Domain.Entities;

namespace OasisApi.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
