using System.ComponentModel.DataAnnotations;

namespace RaceDayAPI.Models
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string ParticipantName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string ParticipantSurname { get; set; } = string.Empty;

        public string? ParticipantEmail { get; set; }

        public int ParticipantContact { get; set; }

        [Required]
        public string Role { get; set; } = "Participant";

        public string? OrganiserAddress { get; set; }
    }
}