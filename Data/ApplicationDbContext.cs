using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using guran_2_2023.Models;

namespace guran_2_2023.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<guran_2_2023.Models.Auto> Auto { get; set; } = default!;
        public DbSet<guran_2_2023.Models.Predajcovia> Predajcovia { get; set; } = default!;
    }
}
