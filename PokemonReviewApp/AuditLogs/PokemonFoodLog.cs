namespace PokemonReviewApp.AuditLogs
{
    public class PokemonFoodLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string PerformedBy { get; set; }
        public int PokemonId { get; set; }
        public int FoodId { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}