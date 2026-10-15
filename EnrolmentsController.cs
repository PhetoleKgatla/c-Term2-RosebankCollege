using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;
using System.Security.Claims;


namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public EnrolmentsController(RaceDayContext context)
        {

            _context = context;
        }

        // GET: api/Enrolments
        [Authorize(Roles = "Organiser")]
        [HttpGet]
        public async Task<IActionResult> GetEnrolments()
        {
            var enrolments = await _context.Enrolments.ToListAsync();
            return Ok(enrolments);
        }

        // GET: api/Enrolments/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrolment(int id)
        {

            {
                var enrolment = await _context.Enrolments
                    .FirstOrDefaultAsync(e => e.EnrolmentID == id);

                if (enrolment == null)
                {
                    return NotFound();
                }

                // Organisers can view enrolments for management.
                if (User.IsInRole("Organiser"))
                {
                    return Ok(enrolment);
                }

                // Participants can only view their own enrolments.
                var userIdValue = User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdValue, out int userId))
                {
                    return Unauthorized();
                }

                var loggedInUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserID == userId);

                if (loggedInUser?.ParticipantID != enrolment.ParticipantID)
                {
                    return Forbid();
                }

                return Ok(enrolment);
            }

        }

        // POST: api/Enrolments
        [Authorize(Roles = "Participant")]
        [HttpPost]
        public async Task<IActionResult> CreateEnrolment(
            Enrolment newEnrolment)
        {

            var userIdValue = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out int userId))
            {
                return Unauthorized();
            }

            var loggedInUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (loggedInUser == null ||
                loggedInUser.ParticipantID == null)
            {
                return Forbid();
            }

            // Use the participant linked to the logged-in account
            newEnrolment.ParticipantID = loggedInUser.ParticipantID.Value;

            _context.Enrolments.Add(newEnrolment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEnrolment),
                new { id = newEnrolment.EnrolmentID },
                newEnrolment);
        }
    }
}
