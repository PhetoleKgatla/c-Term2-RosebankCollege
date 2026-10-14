namespace RaceDay.Models
{
    public class Results
    {
        public int ResultId { get; set; }

        public string FinishTime { get; set; } = "";

        public int FinishingPosition { get; set; }

        public int EnrolmentId { get; set; }
    }
}
