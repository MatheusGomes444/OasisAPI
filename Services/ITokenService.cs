using OasisApi.Models;

namespace OasisApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
