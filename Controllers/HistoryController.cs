using Calavier_backend.Data;
using Calavier_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calavier_backend.Controllers
{
    [ApiController]
    [Route("api/history")]
    public class HistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/history/list
        [HttpGet("list")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _context.Histories
                .Include(h => h.User)
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();

            return Ok(history);
        }
    }
}
