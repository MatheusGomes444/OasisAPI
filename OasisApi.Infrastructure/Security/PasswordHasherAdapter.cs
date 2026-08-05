using Microsoft.AspNetCore.Identity;
using OasisApi.Application.Interfaces;

namespace OasisApi.Infrastructure.Security
{
    public class PasswordHasherAdapter : IPasswordHasher
    {
        // O tipo genérico não importa: a implementação padrão do PasswordHasher
        // não examina a instância de usuário, só o hash/senha.
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string password) =>
            _hasher.HashPassword(new object(), password);

        public bool VerifyPassword(string hashedPassword, string providedPassword) =>
            _hasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword) != PasswordVerificationResult.Failed;
    }
}
