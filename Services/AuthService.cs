using Microsoft.AspNetCore.Identity;
using OasisApi.Common.Exceptions;
using OasisApi.Dtos.Auth;
using OasisApi.Models;
using OasisApi.Repositories;

namespace OasisApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Credenciais inválidas!");
            }

            var token = _tokenService.GenerateToken(user);
            return new AuthResponseDto { Authenticated = true, Token = token };
        }

        public async Task RegisterAsync(RegisterRequestDto dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
            {
                throw new BadRequestException("Usuário já existe.");
            }

            var user = new User
            {
                Email = dto.Email,
                Username = dto.Email,
                PasswordHash = _passwordHasher.HashPassword(null!, dto.Password)
            };

            await _userRepository.AddAsync(user);
        }
    }
}
