using Microsoft.AspNetCore.Mvc;
using Calavier_backend.Models;
using Calavier_backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Calavier_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // REGISTER USER
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                return BadRequest(new { message = "Email already exists" });

            // 🔥 PROFILE PICTURE SAVE
            string? imagePath = null;

            if (dto.ProfilePicture != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profiles");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProfilePicture.FileName);
                var fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ProfilePicture.CopyToAsync(stream);
                }

                imagePath = "/uploads/profiles/" + fileName;
            }

            var user = new User
            {
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                BranchId = dto.BranchId,
                LicenseType = dto.LicenseType,
                Country = dto.Country,
                DOB = dto.DOB,
                Department = dto.Department,
                Gender = dto.Gender,
                UserType = dto.UserType,
                ReportTo = dto.ReportTo,

                MfaRegister = dto.MfaRegister ?? false,
                Status = dto.Status ?? true,
                IpAddress = dto.IpAddress,
                Telephone = dto.Telephone,
                Mobile = dto.Mobile,

                ProfileSelect = string.IsNullOrEmpty(dto.ProfileSelect)
         ? new List<string>()
         : dto.ProfileSelect.Split(',').ToList(),

                FieldVisit = dto.FieldVisit ?? false,
                Signature = dto.Signature ?? false,
                AlwaysBccMyself = dto.AlwaysBccMyself ?? false,

                ProfilePicture = imagePath
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            string branchName = "N/A";
            if (user.BranchId.HasValue)
            {
                branchName = await _context.Branches
                    .Where(b => b.Id == user.BranchId.Value)
                    .Select(b => b.BranchName)
                    .FirstOrDefaultAsync() ?? "N/A";
            }

            var registerHistory = new History
            {
                UserId = user.Id,
                BranchName = "1",
                Action = "USER_REGISTER",
                Remark = $"User {user.Email} registered"
            };

            await _context.Histories.AddAsync(registerHistory);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully",
                user
            });
        }
        [HttpPut("update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(dto.Id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // ✅ Update fields if provided
            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Password)) user.Password = dto.Password;
            if (!string.IsNullOrEmpty(dto.Role)) user.Role = dto.Role;
            if (dto.BranchId.HasValue) user.BranchId = dto.BranchId.Value;
            if (!string.IsNullOrEmpty(dto.LicenseType)) user.LicenseType = dto.LicenseType;
            if (!string.IsNullOrEmpty(dto.Country)) user.Country = dto.Country;
            if (dto.DOB.HasValue) user.DOB = dto.DOB.Value;
            if (!string.IsNullOrEmpty(dto.Department)) user.Department = dto.Department;
            if (!string.IsNullOrEmpty(dto.Gender)) user.Gender = dto.Gender;
            if (!string.IsNullOrEmpty(dto.UserType)) user.UserType = dto.UserType;
            if (!string.IsNullOrEmpty(dto.ReportTo)) user.ReportTo = dto.ReportTo;

            if (dto.MfaRegister.HasValue) user.MfaRegister = dto.MfaRegister.Value;
            if (dto.Status.HasValue) user.Status = dto.Status.Value;
            if (!string.IsNullOrEmpty(dto.IpAddress)) user.IpAddress = dto.IpAddress;
            if (!string.IsNullOrEmpty(dto.Telephone)) user.Telephone = dto.Telephone;
            if (!string.IsNullOrEmpty(dto.Mobile)) user.Mobile = dto.Mobile;
            if (dto.FieldVisit.HasValue) user.FieldVisit = dto.FieldVisit.Value;
            if (dto.Signature.HasValue) user.Signature = dto.Signature.Value;
            if (dto.AlwaysBccMyself.HasValue) user.AlwaysBccMyself = dto.AlwaysBccMyself.Value;

            // 🔥 Update ProfileSelect if provided
            if (!string.IsNullOrEmpty(dto.ProfileSelect))
                user.ProfileSelect = dto.ProfileSelect.Split(',').ToList();

            // 🔥 Update ProfilePicture if a new file is uploaded
            if (dto.ProfilePicture != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profiles");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Delete old picture if exists
                if (!string.IsNullOrEmpty(user.ProfilePicture))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfilePicture.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProfilePicture.FileName);
                var fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ProfilePicture.CopyToAsync(stream);
                }

                user.ProfilePicture = "/uploads/profiles/" + fileName;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            string branchName = "N/A";
            if (user.BranchId.HasValue)
            {
                branchName = await _context.Branches
                    .Where(b => b.Id == user.BranchId.Value)
                    .Select(b => b.BranchName)
                    .FirstOrDefaultAsync() ?? "N/A";
            }

            var updateHistory = new History
            {
                UserId = user.Id,
                BranchName = branchName,
                Action = "USER_UPDATE",
                Remark = "User details updated"
            };

            await _context.Histories.AddAsync(updateHistory);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User updated successfully", user });
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Include(u => u.Branch)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Role,
                    u.BranchId,
                    Branch = u.Branch == null ? null : new
                    {
                        u.Branch.Id,
                        u.Branch.BranchName,
                        u.Branch.BranchCode,
                        u.Branch.City,
                        u.Branch.State,
                        u.Branch.Country
                    },
                    u.Status
                })
                .ToListAsync();

            return Ok(users);
        }

    }
}
