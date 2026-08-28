namespace PokemonReviewApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        //şifreyi bit olarak hashliyor
        public byte[] PasswordHash { get; set; } = new byte[0];

        //birden fazla kullanıcı aynı şifreyi kullanırsa hashleri aynı olmasın diye ekledik
        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}