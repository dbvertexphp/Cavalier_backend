using Calavier_backend.Models;
using Calavier_backend.Data;
using Calavier_backend.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calavier_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.SystemType))
            {
                return BadRequest(new { status = false, message = "Email and SystemType are required." });
            }

            try
            {
                // Note: Agar Role column User table mein nahi hai, toh check hata dein
                var user = await _context.Users
                    .FirstOrDefaultAsync(u =>
                        u.Email != null && u.Email.ToLower().Trim() == dto.Email.ToLower().Trim() &&
                        u.Password == dto.Password);

                if (user == null)
                {
                    return Unauthorized(new { status = false, message = "Incorrect credentials" });
                }

                return Ok(new
                {
                    status = true,
                    message = "Login successful",
                    user = new
                    {
                        user.Id,
                        user.Email,
                        user.EmpCode,
                        FullName = user.FirstName + " " + user.LastName,
                        // Branch navigation property hatne ki wajah se sirf BranchId bhej rahe hain
                        user.BranchId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Database connection error", details = ex.Message });
            }
        }
    }
}