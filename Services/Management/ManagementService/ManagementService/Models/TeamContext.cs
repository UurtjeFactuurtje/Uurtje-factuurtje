using Microsoft.EntityFrameworkCore;

namespace ManagementService.Models
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base(options)
        { }

        public DbSet<TeamModel> Teams { get; set; }
    }
}
