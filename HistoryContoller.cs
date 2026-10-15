
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public HistoryController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/History
        [Authorize(Roles = "Organiser")]
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _context.Histories.ToListAsync();
            return Ok(history);
        }

        // GET: api/History/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryRecord(int id)
        {
            var record = await _context.Histories
                .FirstOrDefaultAsync(h => h.HistoryID == id);

            if (record == null)
            {
                return NotFound("History record not found.");
            }

            return Ok(record);
        }

        // GET: api/History/participant/1
        [Authorize]
        [HttpGet("participant/{participantId}")]
        public async Task<IActionResult> GetParticipantHistory(int participantId)
        {
            var history = await _context.Histories
                .Where(h => h.ParticipantID == participantId)
                .ToListAsync();


            if (!User.IsInRole("Organiser"))
            {
                var userIdValue = User.FindFirst(
                    System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdValue, out int userId))
                {
                    return Unauthorized();
                }

                var loggedInUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserID == userId);

                if (loggedInUser?.ParticipantID != participantId)
                {
                    return Forbid();
                }
            }


            return Ok(history);
        }

        // POST: api/History
        [Authorize(Roles = "Organiser")]
        [HttpPost]
        public async Task<IActionResult> CreateHistory(History newHistory)
        {
            _context.Histories.Add(newHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetHistoryRecord),
                new { id = newHistory.HistoryID },
                newHistory);
        }
    }
}
