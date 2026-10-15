
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public ResultsController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            var results = await _context.Results.ToListAsync();
            return Ok(results);
        }

        // GET: api/Results/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResult(int id)
        {
            var result = await _context.Results
                .FirstOrDefaultAsync(r => r.ResultID == id);

            if (result == null)
            {
                return NotFound("Result not found.");
            }

            return Ok(result);
        }

        // POST: api/Results
        [HttpPost]
        public async Task<IActionResult> CreateResult(
            RaceResult newResult)
        {
            _context.Results.Add(newResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetResult),
                new { id = newResult.ResultID },
                newResult);
        }

        // PUT: api/Results/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(
            int id, RaceResult updatedResult)
        {
            var result = await _context.Results
                .FirstOrDefaultAsync(r => r.ResultID == id);

            if (result == null)
            {
                return NotFound("Result not found.");
            }

            result.Finish_time = updatedResult.Finish_time;
            result.Finishing_Positions = updatedResult.Finishing_Positions;
            result.EnrolmentID = updatedResult.EnrolmentID;

            await _context.SaveChangesAsync();

            return Ok(result);
        }
    }
}
