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

            // Check if BranchCode already exists
            bool exists = await _context.Branches.AnyAsync(b => b.BranchCode == model.BranchCode);
            if (exists)
                return BadRequest(new { message = "Branch Code already exists" });

            await _context.Branches.AddAsync(model);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Branch created successfully",
                data = model
            });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] Branch model)
        {
            if (id != model.Id) return BadRequest();
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Branch updated successfully" });
        }
        // GET: api/branch/list
        [HttpGet("list")]
        public async Task<IActionResult> GetBranches()
        {
            var branches = await _context.Branches.ToListAsync();
            return Ok(branches);
        }
        //branch id
        // GET: api/branch/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            // Database se specific Branch find karna
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null)
            {
                return NotFound(new { message = $"Branch with ID {id} not found." });
            }

            return Ok(branch);
        }

        // 🔥 DELETE: api/branch/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await _context.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound(new { message = "Branch not found" });
            }

            try
            {
                // Optional: Check if any user is assigned to this branch
                bool isLinkedToUser = await _context.Users.AnyAsync(u => u.BranchId == id);
                if (isLinkedToUser)
                {
                    return BadRequest(new { message = "Cannot delete branch. It is currently assigned to one or more users." });
                }

                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Branch deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting branch", error = ex.Message });
            }
        }
    }
}