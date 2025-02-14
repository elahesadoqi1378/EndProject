using Achareh.Domain.Core.Entities.Request;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achareh.Domain.Core.Enums;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class ExpertOfferConfiguration : IEntityTypeConfiguration<ExpertOffer>
    {
        public void Configure(EntityTypeBuilder<ExpertOffer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x =>x.SuggestedPrice)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);


            builder.HasOne(x => x.Request)
                .WithMany(x => x.ExpertOffers)
                .HasForeignKey(x => x.RequestId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasData(
               new ExpertOffer
               {
                   Id = 1,
                   SuggestedPrice = 500000,
                   OfferDate = DateTime.Now.AddDays(3),
                   CreatedAt = new DateTime(2025, 2, 2),
                   IsDeleted = false,
                   OfferStatusEnum = StatusEnum.WorkStarted,
                   Description = "من این خانه را به خوبی نظافت می کنم",
                   RequestId = 1,
                   ExpertId = 1
               },
               new ExpertOffer
               {
                   Id = 2,
                   SuggestedPrice = 750000,
                   OfferDate = DateTime.Now.AddDays(2),
                   CreatedAt = new DateTime(2025, 2, 2),
                   IsDeleted = false,
                   OfferStatusEnum = StatusEnum.WatingExpertOffer,
                   Description = "پیشنهاد برای تعمیر کولر",
                   RequestId = 2,
                   ExpertId = 1
               }
           );
        }
    }

}
