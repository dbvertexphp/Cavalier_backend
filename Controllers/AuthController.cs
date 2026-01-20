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

        // ===========================
        // LOGIN
        // ===========================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check only if Access = "System Administrator"
            if (dto.Access == "System Administrator")
            {
                var admin = await _context.Admin
                    .FirstOrDefaultAsync(a => a.Email == dto.Email && a.Password == dto.Password);

                if (admin == null)
                    return Unauthorized(new { message = "Invalid email or password" });

                return Ok(new
                {
                    message = "Admin login successful",
                    admin = new
                    {
                        admin.Id,
                        admin.Name,
                        admin.Email,
                        admin.PhoneNumber
                    }
                });
            }

            return Unauthorized(new { message = "Access denied. Only System Administrator can login." });
        }





        [HttpPost("verify-branch")]
        public async Task<IActionResult> VerifyBranch([FromBody] BranchVerifyDto dto)
        {
            // 1️⃣ User find karo
            var user = await _context.Users
                .Include(u => u.Branch)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized(new { message = "User not found" });

            // 2️⃣ User ke paas branch hai ya nahi
            if (user.Branch == null)
                return Unauthorized(new { message = "User branch not assigned" });

            // 3️⃣ Branch details verify karo
            if (
                !string.Equals(user.Branch.BranchName, dto.BranchName, StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(user.Branch.CompanyName, dto.CompanyName, StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(user.Branch.City, dto.City, StringComparison.OrdinalIgnoreCase)
            )
            {
                return Unauthorized(new { message = "Branch verification failed" });
            }

            // ✅ Sab match ho gaya
            return Ok(new { message = "Branch verified successfully" });
        }

    }


    // ===========================
    // DTO for login
    // ===========================
    

}
