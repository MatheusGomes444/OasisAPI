namespace OasisApi.Dtos.Auth
{
    public class AuthResponseDto
    {
        public bool Authenticated { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
