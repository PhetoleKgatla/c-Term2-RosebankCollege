using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RaceDay.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Surname { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        public string Contact { get; set; } = "";

        public string? Address { get; set; }

        public string Role { get; set; } = "Participant";

        // JsonIgnore does not return the password hash in API responses
        [JsonIgnore]
        public string PasswordHash { get; set; } = "";
    }
}
