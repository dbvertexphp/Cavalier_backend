using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calavier_backend.Models
{
    public class History
    {
        public int Id { get; set; }

        // FK → Users table
        [Required]
        public int UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [Required]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        public string Action { get; set; } = string.Empty;

        public string? Remark { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
