using Calavier_backend.Models;
using Calavier_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calavier_backend.Controllers
{
    [ApiController]
    [Route("api/branch")]
    public class BranchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/branch/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateBranch([FromBody] Branch model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool exists = await _context.Branches.AnyAsync(b => b.BranchCode == model.BranchCode);
            if (exists)
                return BadRequest("Branch Code already exists");

            await _context.Branches.AddAsync(model);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Branch created successfully",
                data = model
            });
        }

        // GET: api/branch/list
        [HttpGet("list")]
        public async Task<IActionResult> GetBranches()
        {
            var branches = await _context.Branches.ToListAsync();
            return Ok(branches);
        }
    }
}
