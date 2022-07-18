using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Assist.July._2022.BE2.Domain.Configurations
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            builder
                .HasKey(entity => entity.Id);

            builder
                .Property(entity => entity.Id)
                .HasDefaultValueSql("NEWID()");

            builder
                .Property(entity => entity.Title)
                .IsRequired();

            builder
                .Property(entity => entity.Description)
                .IsRequired();

            builder
                .Property(entity => entity.ShortDescription)
                .IsRequired();

            builder
                .Property(entity => entity.Location)
                .IsRequired();

            builder
                .Property(entity => entity.Price)
                .IsRequired();

            builder
                .HasOne(entity => entity.Author)
                .WithMany(entity => entity.Listings);

            builder
                .Property(entity => entity.ApprovedBy);
                //.IsRequired();

            builder
                .Property(entity => entity.Status)
                .IsRequired();

            builder
                .Property(entity => entity.Images)
                .IsRequired();

            builder
                .Property(entity => entity.Category)
                .IsRequired();

            builder
                .Property(entity => entity.ViewCounter)
                .IsRequired();

            builder
                .Property(entity => entity.CreatedAt)
                .IsRequired();

            builder
                .Property(entity => entity.UpdatedAt)
                .IsRequired();
        }
    }
}
