using Microsoft.EntityFrameworkCore;
using MyUser.Models;

namespace MyUser.Models
{
    public class UserContext : DbContext
    {
        public static string ConnectionString = "server=localhost;integrated security=True; database=UserDB;TrustServerCertificate=true;";

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public UserContext()
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.FullName).HasComputedColumnSql($"[{nameof(User.FirstName)}] + ' ' + [{nameof(User.LastName)}]", true);
        }
    }
}
