using System.ComponentModel.DataAnnotations;

namespace Calavier_backend.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required]
        public required string CompanyName { get; set; }

        public string? CompanyAlias { get; set; }

        [Required]
        public required string BranchName { get; set; }

        [Required]
        public required string BranchCode { get; set; }  // Unique short code, e.g., "DEL", "MUM"

        [Required]
        public required string Country { get; set; } = "India";

        [Required]
        public required string TimeZone { get; set; } = "Asia/Kolkata";

        [Required]
        public required string City { get; set; }

        [Required]
        public required string Address { get; set; }  // Typo fix: Address (not Adress)

       

        [Required]
        public required string State { get; set; }

        [Required, MaxLength(10)]
        public required string PostalCode { get; set; }

        [Required]
        public required string ContactNo { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        public string? FaxNumber { get; set; }

        [Required]
        public required string GstCategory { get; set; }  // ITC, etc.

        // New Must-Have Additions
        [Required]
        public required string GSTIN { get; set; }

        public string? IECCode { get; set; }

        public string? DefaultCustomHouseCode { get; set; }  // e.g., INDEL6 for IGI Airport
        [Required]
        public required string CopyDefaultFrom { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
    }
}
