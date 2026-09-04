using System.ComponentModel;

namespace PokemonReviewApp.Dto
{
    public class PokemonInputDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }

        [DefaultValue(null)]
        public byte[]? Photo { get; set; }
    }
}
