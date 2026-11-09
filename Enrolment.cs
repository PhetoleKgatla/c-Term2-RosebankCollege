namespace RaceDay.MVC.Models
{
    public class EnrolmentModel
    {
        public int EnrolmentID { get; set; }

        public int EventID { get; set; }

        public int ParticipantID { get; set; }

        public int OrganiserID { get; set; }

        public int CategoryID { get; set; }

        public string EnrolDate { get; set; } = string.Empty;
    }
}