namespace PokemonReviewApp.AuditLogs
{
    public class CountryLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int CountryId { get; set; }
        public string? NewName { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}
