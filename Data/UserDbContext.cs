using eventz.Data.Map;
using eventz.Models;
using Microsoft.EntityFrameworkCore;

namespace eventz.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
        //public DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
