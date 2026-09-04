public class LoginResponseDto
{
    public string Token { get; set; }
    public string Message { get; set; }
    public string Username { get; set; }
    public List<string> Permissions { get; set; } = new();
}