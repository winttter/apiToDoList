using Microsoft.EntityFrameworkCore;
using WebApplication3.model;

namespace WebApplication3
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<TaskListEntity> taskLists { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
