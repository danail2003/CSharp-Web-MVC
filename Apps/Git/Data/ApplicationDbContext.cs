using Microsoft.EntityFrameworkCore;

namespace Git.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Repository> Repositories { get; set; }

        public virtual DbSet<Commit> Commits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-LM6SUBI\SQLEXPRESS;Database=Git;Integrated Security=true;");
            }
        }
    }
}
