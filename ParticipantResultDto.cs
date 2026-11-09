namespace RaceDayAPI.Models
{
    public class ParticipantResultDto
    {
        public int ResultID { get; set; }

        public int EventID { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string EventDate { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string Finish_time { get; set; } = string.Empty;

        public int Finishing_Positions { get; set; }
    }
}