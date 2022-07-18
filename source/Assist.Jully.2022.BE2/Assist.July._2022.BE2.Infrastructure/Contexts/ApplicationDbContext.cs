using Microsoft.EntityFrameworkCore;
using Assist.July._2022.BE2.Domain.Configurations;
using Assist.July._2022.BE2.Domain.Entities;
namespace Assist.July._2022.BE2.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new ListingConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserActivityConfiguration());

            modelBuilder.Entity<Listing>().Property(p => p.Id).HasDefaultValueSql("NEWID()");
        }

    }
}
