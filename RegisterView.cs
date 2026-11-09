namespace RaceDay.MVC.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ParticipantName { get; set; } = string.Empty;

        public string ParticipantSurname { get; set; } = string.Empty;

        public string? ParticipantEmail { get; set; }

        public int ParticipantContact { get; set; }

        public string Role { get; set; } = "Participant";

        public string? OrganiserAddress { get; set; }
    }
}