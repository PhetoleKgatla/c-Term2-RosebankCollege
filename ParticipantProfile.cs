using System.ComponentModel.DataAnnotations;

namespace RaceDay.MVC.Models
{
    public class ParticipantProfileModel
    {
        public int ParticipantID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string ParticipantName { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Display(Name = "Surname")]
        public string ParticipantSurname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(30)]
        [Display(Name = "Email Address")]
        public string ParticipantEmail { get; set; } = string.Empty;

        [Display(Name = "Contact Number")]
        public int ParticipantContact { get; set; }
    }
}