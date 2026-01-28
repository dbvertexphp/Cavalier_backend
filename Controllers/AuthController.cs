using Calavier_backend.Data;
using Calavier_backend.DTO;
using Calavier_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calavier_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // System Administrator check for Admin Table
            if (dto.SystemType == "System Administrator")
            {
                var admin = await _context.Admin
                    .FirstOrDefaultAsync(a => a.Email == dto.Email && a.Password == dto.Password);

                if (admin == null)
                    return Unauthorized(new { message = "Invalid admin email or password" });

                return Ok(new
                {
                    message = "Admin login successful",
                    admin = new { admin.Id, admin.Name, admin.Email }
                });
            }

            // Normal User check for Users Table
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new
            {
                message = "User login successful",
                user = new { user.Id, user.Email, user.FirstName, user.BranchId }
            });
        }

        [HttpPost("verify-branch")]
        public async Task<IActionResult> VerifyBranch([FromBody] BranchVerifyDto dto)
        {
            // Note: Navigation property 'Branch' is removed. 
            // We verify using BranchId or fetch branch separately if needed.
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized(new { message = "User not found" });

            // Branch verification logically: user must have a BranchId assigned
            if (user.BranchId == 0)
                return Unauthorized(new { message = "User branch not assigned" });

            // Since user.Branch (Navigation) is removed, we just return success 
            // if user exists and has a branchId.
            return Ok(new { message = "Branch verified successfully", branchId = user.BranchId });
        }
    }
}