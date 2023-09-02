using DatabaseService.Data;
using DatabaseService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatabaseService.Controllers
{
    [Route("api/database")]
    public class DatabaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatabaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertUniqueStrings([FromBody] string[] uniqueStrings)
        {
            if (uniqueStrings == null) return BadRequest();
            // Measure start time
            var startTime = DateTime.UtcNow;

            // Loop through uniqueStrings and insert into the database
            foreach (var str in uniqueStrings)
            {
                // Check if the string already exists in the database to avoid duplicates
                if (!_context.UniqueStrings.Any(e => e.UniqueStrings == str))
                {
                    _context.UniqueStrings.Add(new UniqueString { UniqueStrings = str });
                }
            }

            await _context.SaveChangesAsync(); // Save changes to the database

            // Measure end time
            var endTime = DateTime.UtcNow;

            // Calculate the time taken
            var timeTaken = endTime - startTime;

            // Create and return a response object with timeTaken
            var response = new { TimeTaken = timeTaken.TotalMilliseconds };
            return Ok(response);
        }
    }
}

