using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("ENROLMENT")]
    public class Enrolment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnrolmentID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public int ParticipantID { get; set; }

        [Required]
        public int OrganiserID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required, MaxLength(9)]
        public string EnrolDate { get; set; } = string.Empty;
    }
}
