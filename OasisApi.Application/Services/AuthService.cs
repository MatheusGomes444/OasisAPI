using OasisApi.Application.Dtos.Auth;
using OasisApi.Application.Exceptions;
using OasisApi.Application.Interfaces;
using OasisApi.Application.Interfaces.Repositories;
using OasisApi.Domain.Entities;

namespace OasisApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, dto.Password))
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
                PasswordHash = _passwordHasher.HashPassword(dto.Password)
            };

            await _userRepository.AddAsync(user);
        }
    }
}
