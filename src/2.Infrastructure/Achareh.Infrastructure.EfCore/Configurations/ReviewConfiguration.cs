using Achareh.Domain.Core.Entities.Request;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(1000);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Expert)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);

           
                  

            builder.HasData

           (
               new Review
               {
                   Id = 1,
                   Title = "عالی",
                   CustomerId = 1,
                   ExpertId = 1,
                   IsAccept = false,
                   Comment = "از این کار راضی بودم",
                   IsDeleted = false,
                   Rating = 4,
                   CreatedAt = new DateTime(2025, 2, 2),
                  
               }
           );
            

        }
    }

}
