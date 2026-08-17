namespace PokemonReviewApp.OutputDtos
{
    public class ReviewOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public decimal Rating { get; set; }

        //review'in hangi pokemon için yapıldığını da gösterebilmek için
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }

        public int ReviewerId { get; set; }
        public string ReviewerFirstName { get; set; }
        public string ReviewerLastName { get; set; }

    }
}
