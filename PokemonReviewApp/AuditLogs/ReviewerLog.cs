namespace PokemonReviewApp.AuditLogs
{
    public class ReviewerLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int ReviewerId { get; set; }
        public string? OldFirstName { get; set; }
        public string? OldLastName { get; set; }
        public string? NewFirstName { get; set; }
        public string? NewLastName { get; set; }
        public DateTime LoggedAt { get; set; }

    }
}
