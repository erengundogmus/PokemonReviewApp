namespace PokemonReviewApp.AuditLogs
{
    public class OwnerLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string PerformedBy { get; set; }
        public int OwnerId { get; set; }
        public string? NewName { get; set; }
        public string? NewGym { get; set; }
        public DateTime LoggedAt { get; set; }

    }
}
