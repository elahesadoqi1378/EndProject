﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Achareh.Domain.Core.Entities.BaseEntities;


namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.ToTable("Cities");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.Requests)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new List<City>()
            {
                new City() { Id = 1, Title = "تهران" },
                new City() { Id = 2, Title = "مشهد" },
                new City() { Id = 3, Title = "اصفهان" },
                new City() { Id = 4, Title = "شیراز" },
                new City() { Id = 5, Title = "تبریز" },
                new City() { Id = 6, Title = "کرج" },
                new City() { Id = 7, Title = "قم" },
                new City() { Id = 8, Title = "اهواز" },
                new City() { Id = 9, Title = "اردبیل" },
                new City() { Id = 10, Title = "کرمانشاه" },
                new City() { Id = 11, Title = "زاهدان" },
                new City() { Id = 12, Title = "ارومیه" },
                new City() { Id = 13, Title = "یزد" },
                new City() { Id = 14, Title = "همدان" },
                new City() { Id = 15, Title = "قزوین" },
                new City() { Id = 16, Title = "سنندج" },
                new City() { Id = 17, Title = "بندرعباس" },
                new City() { Id = 18, Title = "زنجان" },
                new City() { Id = 19, Title = "ساری" },
                new City() { Id = 20, Title = "بوشهر" },
                new City() { Id = 21, Title = "مازندران" },
                new City() { Id = 22, Title = "گرگان" },
                new City() { Id = 23, Title = "کرمان" },
                new City() { Id = 24, Title = "خرم آباد" },
                new City() { Id = 25, Title = "سمنان" }
            });
        }
    }

}
