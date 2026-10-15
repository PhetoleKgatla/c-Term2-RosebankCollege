using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Authorize(Roles = "Organiser")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisersController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public OrganisersController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/Organisers
        [HttpGet]
        public async Task<IActionResult> GetOrganisers()
        {
            var organisers = await _context.Organisers.ToListAsync();
            return Ok(organisers);
        }

        // GET: api/Organisers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganiser(int id)
        {
            var organiser = await _context.Organisers
                .FirstOrDefaultAsync(o => o.OrganiserID == id);

            if (organiser == null)
            {
                return NotFound("Organiser not found.");
            }

            return Ok(organiser);
        }

        // POST: api/Organisers
        [HttpPost]
        public async Task<IActionResult> CreateOrganiser(
            Organiser newOrganiser)
        {
            _context.Organisers.Add(newOrganiser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOrganiser),
                new { id = newOrganiser.OrganiserID },
                newOrganiser);
        }

        // PUT: api/Organisers/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganiser(
            int id, Organiser updatedOrganiser)
        {
            var organiser = await _context.Organisers
                .FirstOrDefaultAsync(o => o.OrganiserID == id);

            if (organiser == null)
            {
                return NotFound("Organiser not found.");
            }

            organiser.OrganiserName = updatedOrganiser.OrganiserName;
            organiser.OrganiserSurname = updatedOrganiser.OrganiserSurname;
            organiser.OrganiserAddress = updatedOrganiser.OrganiserAddress;

            await _context.SaveChangesAsync();

            return Ok(organiser);
        }

        // DELETE: api/Organisers/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganiser(int id)
        {
            var organiser = await _context.Organisers
                .FirstOrDefaultAsync(o => o.OrganiserID == id);

            if (organiser == null)
            {
                return NotFound("Organiser not found.");
            }

            _context.Organisers.Remove(organiser);
            await _context.SaveChangesAsync();

            return Ok("Organiser deleted successfully.");
        }
    }
}
