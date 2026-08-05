using OasisApi.Dtos.Auth;

namespace OasisApi.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task RegisterAsync(RegisterRequestDto dto);
    }
}
