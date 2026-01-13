using Calavier_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Calavier_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; } // ✅ Correct
    }
}
