using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("RESULTS")]
    public class RaceResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResultID { get; set; }

        [Required, Column("Finish_time"), MaxLength(20)]
        public string Finish_time { get; set; } = string.Empty;

        [Required]
        public int Finishing_Positions { get; set; }

        [Required]
        public int EnrolmentID { get; set; }
    }
}
