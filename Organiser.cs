using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class Organiser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganiserID { get; set; }

        [Required, MaxLength(30)]
        public string OrganiserName { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string OrganiserSurname { get; set; } = string.Empty;

        [Required, MaxLength(60)]
        public string OrganiserAddress { get; set; } = string.Empty;
    }
}
