namespace OasisApi.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public Guid Uuid { get; set; } = UuidV7.NewGuid();
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } // Armazenar o hash da senha
}