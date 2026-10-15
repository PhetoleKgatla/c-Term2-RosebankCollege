using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("EVENTS")]
    public class RaceEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventID { get; set; }

        [Required, MaxLength(80)]
        public string EventName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required, Column("Date"), MaxLength(9)]
        public string Date { get; set; } = string.Empty;

        [Required, Column("Location"), MaxLength(40)]
        public string Location { get; set; } = string.Empty;

        [Required, MaxLength(5000)]
        public string Distance { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? EventType { get; set; }

        [Required]
        public int OrganiserID { get; set; }
    }
}
