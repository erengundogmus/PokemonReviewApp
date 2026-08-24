namespace PokemonReviewApp.OutputDtos
{
    public class PokemonOutputDto
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
