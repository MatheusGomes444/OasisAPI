public class User
{
    public int Id { get; set; }
    public Guid Uuid { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } // Armazenar o hash da senha
}