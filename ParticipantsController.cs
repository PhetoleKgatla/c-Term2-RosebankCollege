
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public ParticipantsController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/Participants
        [HttpGet]
        public async Task<IActionResult> GetParticipants()
        {
            var participants = await _context.Participants.ToListAsync();
            return Ok(participants);
        }

        // GET: api/Participants/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipant(int id)
        {
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.ParticipantID == id);

            if (participant == null)
            {
                return NotFound("Participant not found.");
            }

            return Ok(participant);
        }

        // POST: api/Participants
        [HttpPost]
        public async Task<IActionResult> CreateParticipant(
            Participant newParticipant)
        {
            _context.Participants.Add(newParticipant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetParticipant),
                new { id = newParticipant.ParticipantID },
                newParticipant);
        }

        // PUT: api/Participants/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipant(
            int id, Participant updatedParticipant)
        {
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.ParticipantID == id);

            if (participant == null)
            {
                return NotFound("Participant not found.");
            }

            participant.ParticipantName = updatedParticipant.ParticipantName;
            participant.ParticipantSurname = updatedParticipant.ParticipantSurname;
            participant.ParticipantEmail = updatedParticipant.ParticipantEmail;
            participant.ParticipantContact = updatedParticipant.ParticipantContact;

            await _context.SaveChangesAsync();

            return Ok(participant);
        }

        // DELETE: api/Participants/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.ParticipantID == id);

            if (participant == null)
            {
                return NotFound("Participant not found.");
            }

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            return Ok("Participant deleted successfully.");
        }
    }
}
