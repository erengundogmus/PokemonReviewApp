namespace PokemonReviewApp.AuditLogs
{
    public class CategoryLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string PerformedBy { get; set; }
        public int CategoryId { get; set; }
        public string? NewName { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}