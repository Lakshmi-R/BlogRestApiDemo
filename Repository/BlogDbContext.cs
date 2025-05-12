using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Models;


namespace Repository
{
    public class BlogDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<User> Users { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
               new User { Id = 1, UserName = "admin", Password = "password", Role = "Admin" }
                        );
        }
    }
}

