
namespace RaceDay.Models
{
    public class Enrolment
    {
        public int EnrolmentId { get; set; }

        public int EventId { get; set; }

        // The participant is stored as a User
        public int UserId { get; set; }

        public int OrganiserId { get; set; }

        public int CategoryId { get; set; }

        public DateTime EnrolDate { get; set; } = DateTime.UtcNow;
    }
}
