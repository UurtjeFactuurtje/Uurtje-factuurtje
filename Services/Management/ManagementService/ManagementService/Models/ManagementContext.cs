using Microsoft.EntityFrameworkCore;


namespace ManagementService.Models
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        { }

        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<PeopleModel> People { get; set; }

    }
}
