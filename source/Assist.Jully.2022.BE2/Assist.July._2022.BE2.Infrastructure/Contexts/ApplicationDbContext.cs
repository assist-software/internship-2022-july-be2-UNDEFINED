using Microsoft.EntityFrameworkCore;
using Assist.July._2022.BE2.Domain.Configurations;
namespace Assist.July._2022.BE2.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new ListingConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
        }
    }
}
