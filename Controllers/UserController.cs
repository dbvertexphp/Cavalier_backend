using Microsoft.AspNetCore.Mvc;
using Calavier_backend.Models;
using Calavier_backend.Data;
using Microsoft.EntityFrameworkCore;
using Calavier_backend.DTO;

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

        // --- 1. GET ALL USERS ---
        [HttpGet("list")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // --- 2. GET USER BY ID ---
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound(new { message = $"User with ID {id} not found." });
            return Ok(user);
        }

        // --- 3. REGISTER USER ---
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] UserRegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null) return BadRequest(new { message = "Email already exists" });

            string? imagePath = null;
            if (dto.Photo != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Photo.FileName);
                var fullPath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create)) { await dto.Photo.CopyToAsync(stream); }
                imagePath = "/uploads/" + fileName;
            }

            var user = new User
            {
                EmpCode = dto.EmpCode,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                DOB = dto.DOB,
                DateOfJoining = dto.DateOfJoining,
                CTC_Monthly = dto.CTC_Monthly,
                Designation = dto.Designation,
                PAN_No = dto.PAN_No,
                AadhaarNo = dto.AadhaarNo,
                ContactPersonal = dto.Mobile,
                BranchId = dto.BranchId,
                PhotoPath = imagePath,
                Status = true,

                // --- Mapping the 16 New Fields ---
                RoleId = dto.RoleId,
                LicenceType = dto.LicenceType,
                Country = dto.Country,
                Department = dto.Department,
                Gender = dto.Gender,
                UserType = dto.UserType,
                ReportTo = dto.ReportTo,
                MfaRegistration = dto.MfaRegistration,
                IPAdress = dto.IPAdress,
                ProfilePicture = dto.ProfilePicture,
                Telephone = dto.Telephone,
                Mobile = dto.Mobile,
                ProfileSelect = dto.ProfileSelect,
                FieldVisit = dto.FieldVisit,
                Signature = dto.Signature,
                AlwaysBccmyself = dto.AlwaysBccmyself,

                // Legacy fields
                TenthYear = dto.TenthYear,
                TwelfthYear = dto.TwelfthYear,
                GraduationYear = dto.GraduationYear,
                PostGraduationYear = dto.PostGraduationYear,
                BloodGroup = dto.BloodGroup,
                SalaryAccountNo = dto.SalaryAccountNo,
                MaritalStatus = dto.MaritalStatus,
                EmergencyName = dto.EmergencyName,
                EmergencyRelation = dto.EmergencyRelation,
                EmergencyContactNo = dto.EmergencyContactNo,
                SourceOfSelection = dto.SourceOfSelection
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User registered successfully", userId = user.Id });
        }

        // --- 4. UPDATE USER ---
        [HttpPut("update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(dto.Id);
            if (user == null) return NotFound(new { message = "User not found" });

            user.EmpCode = dto.EmpCode ?? user.EmpCode;
            user.FirstName = dto.FirstName ?? user.FirstName;
            user.LastName = dto.LastName ?? user.LastName;
            user.Email = dto.Email ?? user.Email;
            user.Designation = dto.Designation ?? user.Designation;
            user.BranchId = dto.BranchId;

            // --- Update the 16 New Fields ---
            user.RoleId = dto.RoleId ?? user.RoleId;
            user.LicenceType = dto.LicenceType ?? user.LicenceType;
            user.Country = dto.Country ?? user.Country;
            user.Department = dto.Department ?? user.Department;
            user.Gender = dto.Gender ?? user.Gender;
            user.UserType = dto.UserType ?? user.UserType;
            user.ReportTo = dto.ReportTo ?? user.ReportTo;
            user.MfaRegistration = dto.MfaRegistration;
            user.IPAdress = dto.IPAdress ?? user.IPAdress;
            user.ProfilePicture = dto.ProfilePicture ?? user.ProfilePicture;
            user.Telephone = dto.Telephone ?? user.Telephone;
            user.Mobile = dto.Mobile ?? user.Mobile;
            user.ProfileSelect = dto.ProfileSelect ?? user.ProfileSelect;
            user.FieldVisit = dto.FieldVisit;
            user.Signature = dto.Signature ?? user.Signature;
            user.AlwaysBccmyself = dto.AlwaysBccmyself;

            if (dto.Photo != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Photo.FileName);
                var fullPath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create)) { await dto.Photo.CopyToAsync(stream); }
                user.PhotoPath = "/uploads/" + fileName;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User updated successfully" });
        }

        // --- 5. DELETE USER ---
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "User not found" });
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User deleted successfully" });
        }
    }
}