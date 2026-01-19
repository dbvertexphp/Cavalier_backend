using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Calavier_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }

        public string? Role { get; set; }

        public int? BranchId { get; set; }

        // 🔥 Navigation Property
        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }

        public string? LicenseType { get; set; }
        public string? Country { get; set; }
        public DateTime? DOB { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public string? UserType { get; set; }
        public string? ReportTo { get; set; }

        public bool? MfaRegister { get; set; } = false;
        public bool? Status { get; set; } = true;

        public string? IpAddress { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Telephone { get; set; }
        public string? Mobile { get; set; }

        public List<string>? ProfileSelect { get; set; } = new List<string>();
        public bool? FieldVisit { get; set; } = false;
        public bool? Signature { get; set; } = false;
        public bool? AlwaysBccMyself { get; set; } = false;
    }
}
