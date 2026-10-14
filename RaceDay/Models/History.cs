namespace RaceDay.Models
{
    public class History
    {
        public int HistoryId { get; set; }

        public int ResultId { get; set; }

        public int UserId { get; set; }

        public DateTime ViewDate { get; set; } = DateTime.UtcNow;
    }
}
