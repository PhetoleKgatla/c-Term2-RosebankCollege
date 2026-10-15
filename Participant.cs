using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    
        [Table("PARTICIPANTS")]
        public class Participant
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int ParticipantID { get; set; }

            [Required, MaxLength(30)]
            public string ParticipantName { get; set; } = string.Empty;

            [Required, MaxLength(30)]
            public string ParticipantSurname { get; set; } = string.Empty;

            [MaxLength(30)]
            public string? ParticipantEmail { get; set; }

            [Required]
            public int ParticipantContact { get; set; }
        }
}
