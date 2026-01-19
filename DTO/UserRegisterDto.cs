using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Calavier_backend.Models
{
    public class UserRegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;

        [Required]
        public int BranchId { get; set; }

        public string? LicenseType { get; set; }
        public string? Country { get; set; }
        public DateTime? DOB { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public string? UserType { get; set; }
        public string? ReportTo { get; set; }

        public bool? MfaRegister { get; set; }
        public bool? Status { get; set; }
        public string? IpAddress { get; set; }

        // ✅ FILE (Swagger me dikhega)
        public IFormFile? ProfilePicture { get; set; }

        public string? Telephone { get; set; }
        public string? Mobile { get; set; }

        // 👇 string instead of List
        public string? ProfileSelect { get; set; }

        public bool? FieldVisit { get; set; }
        public bool? Signature { get; set; }
        public bool? AlwaysBccMyself { get; set; }
    }
}
