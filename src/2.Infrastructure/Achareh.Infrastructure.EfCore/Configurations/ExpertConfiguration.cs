using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.ExpertOffers)
            .WithOne(x => x.Expert)
            .HasForeignKey(x => x.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
           .WithOne(x => x.Expert)
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasData
           (
                new Expert
                {
                    Id = 1,
                    UserId = 3,
                },
                new Expert
                {
                    Id = 2,
                    UserId = 4,
                },
                 new Expert
                 {
                     Id = 3,
                     UserId = 6,
                 }

           );
            builder
           .HasMany(x => x.HomeServices)
           .WithMany(x => x.Experts)
           .UsingEntity(x => x.HasData
           (
           new { ExpertsId = 1, HomeServicesId = 1 },
           new { ExpertsId = 1, HomeServicesId = 59 },
           new { ExpertsId = 3, HomeServicesId = 2 },
           new { ExpertsId = 2, HomeServicesId = 2 }
           ));
        }
    }
}
