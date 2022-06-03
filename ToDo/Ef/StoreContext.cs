using Microsoft.EntityFrameworkCore;

namespace ToDo.Models
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) 
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set;}
    }
}