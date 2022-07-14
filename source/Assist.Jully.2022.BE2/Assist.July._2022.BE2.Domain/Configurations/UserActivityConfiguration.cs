using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Assist.July._2022.BE2.Domain.Configurations
{
    public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder
                .HasKey(entity => entity.Id);

            builder
                .Property(entity => entity.Device)
                .IsRequired();

            builder
                .Property(entity => entity.DeviceType)
                .IsRequired();

            builder
                .Property(entity => entity.Location)
                .IsRequired();

            builder.
                Property(entity => entity.ConnectionDate).
                IsRequired();

            builder.
                Property(entity => entity.Status).
                IsRequired();
        }
    }
}
