﻿using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasOne(x => x.Customer)
            .WithMany(x => x.Requests)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Review)
                 .WithOne(x => x.Request)
                 .OnDelete(DeleteBehavior.NoAction);
                


            builder.HasData
           (

            new Request
            {
               Id = 1,
               CreatedAt = new DateTime(2025, 2, 2),
               CustomerId = 1,
               IsDeleted = false,
               Description = " تمیزکاری خانه",
               RequestForTime = new DateTime(2025, 3, 5),
               HomeServiceId = 1,
               Title = "نظافت",
               CityId = 1,
               RequestStatus = StatusEnum.WorkPaidByCustomer,
               ReviewId = 1
            },

              new Request
              {
                  Id = 2,
                  CreatedAt = new DateTime(2025, 2, 2),
                  CustomerId = 1,
                  IsDeleted = false,
                  Description = "کودک ",
                  RequestForTime = new DateTime(2025, 3, 5),
                  HomeServiceId = 59,
                  Title = "طراحی اتاق کودک",
                  CityId = 1,
                  RequestStatus = StatusEnum.WatingForChoosingExpert,
                 
              }
           );
        }
    }
}
