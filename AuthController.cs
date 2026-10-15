using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;


namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayContext _context;
        private readonly PasswordHasher<AppUser> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthController(
            RaceDayContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<AppUser>();
        }


        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request)
        {
            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == request.Email);

            if (emailExists)
            {
                return Conflict("This email is already registered.");
            }

            bool participantEmailExists =
                await _context.Participants.AnyAsync(
                    p => p.ParticipantEmail == request.Email);

            if (participantEmailExists)
            {
                return Conflict(
                    "This email is already linked to a participant.");
            }

            int nextId = (await _context.Participants
                .Select(p => (int?)p.ParticipantID)
                .MaxAsync() ?? 0) + 1;

            var participant = new Participant
            {
                ParticipantID = nextId,
                ParticipantName = request.ParticipantName,
                ParticipantSurname = request.ParticipantSurname,
                ParticipantEmail = request.Email,
                ParticipantContact = request.ParticipantContact
            };

            _context.Participants.Add(participant);

            var user = new AppUser
            {
                Email = request.Email,
                Role = "Participant",
                ParticipantID = nextId,
                OrganiserID = null
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user, request.Password);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("Participant account created successfully.");
        }


        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized(
                    "Invalid email or password.");
            }

            var passwordResult =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(
                    "Invalid email or password.");
            }


            if (user.Role == "Organiser" && !user.IsApproved)
            {
                return StatusCode(
                    403,
                    "Your organiser account is awaiting approval.");
            }


            var claims = new[]
            {
    new Claim(
        ClaimTypes.NameIdentifier,
        user.UserID.ToString()),

    new Claim(
        ClaimTypes.Email,
        user.Email),

    new Claim(
        ClaimTypes.Role,
        user.Role)
};

            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(key) ||
                string.IsNullOrWhiteSpace(issuer) ||
                string.IsNullOrWhiteSpace(audience))
            {
                return StatusCode(
                    500,
                    "JWT configuration is missing.");
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                message = "Login successful.",
                token = tokenString,
                userID = user.UserID,
                email = user.Email,
                role = user.Role
            });

        }

    }
}
