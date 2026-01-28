using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calavier_backend.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Yeh line add karein
        public int Id { get; set; }

        // ================= COMPANY DETAILS =================

        [Required]
        public string CompanyName { get; set; } = null!;

        public string? CompanyAlias { get; set; }

        // ================= BRANCH DETAILS =================

        [Required]
        public string BranchName { get; set; } = null!;

        [Required]
        public string BranchCode { get; set; } = null!;   // e.g. DEL001

        [Required]
        public string Country { get; set; } = "India";

        [Required]
        public string TimeZone { get; set; } = "Asia/Kolkata";

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required, MaxLength(10)]
        public string PostalCode { get; set; } = null!;

        [Required]
        public string ContactNo { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string? FaxNumber { get; set; }

        // ================= TAX DETAILS =================

        [Required]
        public string GstCategory { get; set; } = null!;

        [Required, MaxLength(15)]
        public string GSTIN { get; set; } = null!;

        public string? IECCode { get; set; }

        public string? DefaultCustomHouseCode { get; set; }

        [Required]
        public string CopyDefaultFrom { get; set; } = null!;

        // ================= STATUS & AUDIT =================

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; }

        // ================= RELATIONSHIPS =================

        // 🔥 ONE BRANCH → MANY USERS
        public ICollection<User>? Users { get; set; }
    }
}
