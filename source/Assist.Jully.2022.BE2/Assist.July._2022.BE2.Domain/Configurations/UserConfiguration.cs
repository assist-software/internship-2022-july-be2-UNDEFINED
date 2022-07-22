using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assist.July._2022.BE2.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(entity => entity.Id);

            builder
                .Property(entity => entity.FullName)
                .IsRequired();

            builder
                .Property(entity => entity.Email)
                .IsRequired(); 

            builder
                .Property(entity => entity.Password)
                .IsRequired();

            builder
                .Property(entity => entity.Gender)
                .IsRequired();
            
            builder
                .Property(entity => entity.Phone)
                .IsRequired();

            builder
                .Property(entity => entity.Role)
                .IsRequired();

            builder
                .Property(entity => entity.DateOfBirth)
                .IsRequired();

            builder
                .Property(entity => entity.Address)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(entity => entity.Photo);

            builder
                .Property(entity => entity.IsActive);

            builder
                .HasOne(entity => entity.UserActivities)
                .WithMany(entity => entity.Users);

            builder
                .Property(entity => entity.Google);

            builder
                .Property(entity => entity.CreatedAt)
                .IsRequired();

            builder
                .Property(entity => entity.UpdatedAt)
                .IsRequired();
        }
    }
}
