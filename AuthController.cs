
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
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

        public AuthController(RaceDayContext context)
        {
            _context = context;
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
    }
}
