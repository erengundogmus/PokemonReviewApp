namespace PokemonReviewApp.OutputDtos
{
    public class UserLoginOutputDto
    {
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new();
    }
}