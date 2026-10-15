using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("HISTORY")]
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HistoryID { get; set; }

        [Required]
        public int ResultID { get; set; }

        [Required]
        public int ParticipantID { get; set; }

        [Required, MaxLength(9)]
        public string ViewDate { get; set; } = string.Empty;
    }
}
