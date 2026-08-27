namespace PokemonReviewApp.AuditLogs
{
    public class FoodLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int FoodId { get; set; }
        public string? NewName { get; set; }
        public int? NewHp { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}