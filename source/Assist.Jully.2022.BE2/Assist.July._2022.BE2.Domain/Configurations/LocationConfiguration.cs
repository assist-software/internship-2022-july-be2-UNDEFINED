using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assist.July._2022.BE2.Domain.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
                .HasKey(entity => entity.Id);

            builder
                .Property(entity => entity.City)
                .IsRequired();

            builder
                .Property(entity => entity.Country)
                .IsRequired();

            builder
                .Property(entity => entity.State)
                .IsRequired();

            builder
                .Property(entity => entity.Lat)
                .IsRequired();

            builder
                .Property(entity => entity.Lng)
                .IsRequired();

            builder
                .Property(entity => entity.Zip)
                .IsRequired();
        }
    }
}
