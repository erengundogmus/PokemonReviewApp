namespace PokemonReviewApp.AuditLogs
{
    public class ReviewLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int ReviewId { get; set; }
        public string? NewTitle { get; set; }
        public string? NewText { get; set; }
        public int? NewRating { get; set; }
        public int? NewReviewerId { get; set; }
        public int? NewPokemonId { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}
