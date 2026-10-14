using System.ComponentModel.DataAnnotations;

namespace RaceDay.DTOs
{
    public class Register
    {
        public class RegisterRequest
        {
            [Required]
            public string Name { get; set; } = "";

            [Required]
            public string Surname { get; set; } = "";

            [Required, EmailAddress]
            public string Email { get; set; } = "";

            [Required, MinLength(8)]
            public string Password { get; set; } = "";

            [Required]
            public string Role { get; set; } = "Participant";

            public string Contact { get; set; } = "";

            public string? Address { get; set; }
        }
    }
}