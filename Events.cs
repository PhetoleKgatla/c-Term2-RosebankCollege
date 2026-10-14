using System.ComponentModel.DataAnnotations;

namespace RaceDay.Models
{
    public class Events
    {
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime Date { get; set; }

        public string Location { get; set; } = "";

        public string Distance { get; set; } = "";

        // run, walk, or cycle
        public string EventType { get; set; } = "run";

        // The user who organises the event
        public int OrganiserId { get; set; }
    }
}
