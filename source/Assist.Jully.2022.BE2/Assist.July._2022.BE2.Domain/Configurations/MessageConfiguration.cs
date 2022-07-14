using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Assist.July._2022.BE2.Domain.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .Property(entity => entity.SenderId)
                .IsRequired();

            builder
                .Property(entity => entity.ReceiverId)
                .IsRequired();

            builder
                .Property(entity => entity.ListingId)
                .IsRequired();

            builder
               .Property(entity => entity.CreatedAt)
               .IsRequired();

            builder
                .Property(entity => entity.UpdatedAt)
                .IsRequired();

            builder
                .Property(entity => entity.Content)
                .IsRequired();

            
        }
    }
}
