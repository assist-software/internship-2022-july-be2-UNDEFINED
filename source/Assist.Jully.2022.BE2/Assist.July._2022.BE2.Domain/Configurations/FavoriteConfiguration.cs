using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assist.July._2022.BE2.Domain.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder
                .HasOne(entity => entity.Users)
                .WithMany(entity => entity.Favorites);

            builder
                .HasOne(entity => entity.Listings)
                .WithMany(entity => entity.FavoredBy);
        }
    }
}
