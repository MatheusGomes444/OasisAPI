using OasisApi.Application.Dtos.Auth;

namespace OasisApi.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task RegisterAsync(RegisterRequestDto dto);
    }
}
