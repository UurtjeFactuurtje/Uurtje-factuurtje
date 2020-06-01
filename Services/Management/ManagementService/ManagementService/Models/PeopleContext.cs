using Microsoft.EntityFrameworkCore;

namespace ManagementService.Models
{
    public class PeopleContext : DbContext
    {
        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        { }

        public DbSet<PeopleModel> People { get; set; }
    }


}
