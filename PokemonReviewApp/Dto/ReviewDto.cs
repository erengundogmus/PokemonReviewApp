namespace PokemonReviewApp.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public decimal Rating { get; set; }

        //review'in hangi pokemon için yapıldığını da gösterebilmek için
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }

    }
}
