
using Achareh.Domain.Core.Entities.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class HomeServiceConfiguration : IEntityTypeConfiguration<HomeService>
    {
        public void Configure(EntityTypeBuilder<HomeService> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("HomeServices");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);


            builder.HasOne(x => x.SubCategory)
               .WithMany(x => x.HomeServices)
               .HasForeignKey(x => x.SubCategoryId)
               .OnDelete(DeleteBehavior.Cascade);




            builder.HasMany(x => x.Requests)
                 .WithOne(x => x.HomeService);


            builder.HasData(new List<HomeService>()
            {
                new HomeService() { Id = 1, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "خدمات نظافت منزل",Price=2000, SubCategoryId=1 /*, ImagePath = ""*/},
                new HomeService() { Id = 2, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "نظافت راه پله",Price=2000,SubCategoryId=1 /*, ImagePath = ""*/ },
                new HomeService() { Id = 3, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "قالیشویی",Price=2000,SubCategoryId=2 /*,ImagePath = "" */},
                new HomeService() { Id = 4, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "پرده شویی",Price=2000,SubCategoryId=2 /*,ImagePath = "" */},
                new HomeService() { Id = 5, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "سرامیک خودرو",Price=2000,SubCategoryId=3 /*,ImagePath = "" */},
                new HomeService() { Id = 6, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "صفرشویی خودرو",Price=2000,SubCategoryId=3 /*,ImagePath = "" */},
                new HomeService() { Id = 7, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیر و سرویس کولر آبی",Price=2000,SubCategoryId=4 /*,ImagePath = "" */},
                new HomeService() { Id = 8, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کانال سازی کولر",Price=2000,SubCategoryId=4 /*,ImagePath = "" */},
                new HomeService() { Id = 9, CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیر و نگهداری موتورخانه",Price=2000,SubCategoryId=4 /*,ImagePath = "" */},
                new HomeService() { Id = 10,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "سنگ کاری",Price=2000,SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 11,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "بنایی",Price=2000,SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 12,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کلیدسازی",Price=2000,SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 13,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کفسابی",Price=2000,SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 14,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "خدمات لوله کشی ساختمان",Price=2000,SubCategoryId=6 /*,ImagePath = "" */},
                new HomeService() { Id = 15,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تخلیه چاه و لوله بازکنی",Price=2000,SubCategoryId=6 /*,ImagePath = "" */},
                new HomeService() { Id = 16,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "لوله کشی آب و فاضلاب",Price=2000,SubCategoryId=6 /*,ImagePath = "" */},
                new HomeService() { Id = 17,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "مشاوره و بازسازی ساختمان",Price=2000,SubCategoryId=7 /*,ImagePath = "" */},
                new HomeService() { Id = 18,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "دکوراسیون و طراحی ساختمان",Price=2000,SubCategoryId=7 /*,ImagePath = "" */},
                new HomeService() { Id = 19,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "خدمات باغبانی",Price=2000,SubCategoryId=8 /*,ImagePath = "" */},
                new HomeService() { Id = 20,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کاشت و تعویض گلدان",Price=2000,SubCategoryId=8 /*,ImagePath = "" */},
                new HomeService() { Id = 21,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیرات مبلمان",Price=2000,SubCategoryId=9 /*,ImagePath = "" */},
                new HomeService() { Id = 22,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیرات مبلمان اداری",Price=2000,SubCategoryId=9 /*,ImagePath = "" */},
                new HomeService() { Id = 23,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیر پنکه",Price=2000,SubCategoryId=10 /*,ImagePath = "" */},
                new HomeService() { Id = 24,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "نصب و تعمیر فر",Price=2000,SubCategoryId=10 /*,ImagePath = "" */},
                new HomeService() { Id = 25,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیر کامپیوتر و لپ تاپ",Price=2000,SubCategoryId=11 /*,ImagePath = "" */},
                new HomeService() { Id = 26,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "مودم و اینترنت",Price=2000,SubCategoryId=11 /*,ImagePath = "" */},
                new HomeService() { Id = 27,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "خدمات تعمیر موبایل",Price=2000,SubCategoryId=12 /*,ImagePath = "" */},
                new HomeService() { Id = 28,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "خدمات خرید موبایل و کالاهای دیجیتال",Price=2000,SubCategoryId= 12/*,ImagePath = "" */},
                new HomeService() { Id = 29,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "خدمات دوربین",Price=2000,SubCategoryId=12 /*,ImagePath = "" */},
                new HomeService() { Id = 30,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "اسباب کشی با خاور و کامیون",Price=2000,SubCategoryId=13 /*,ImagePath = "" */},
                new HomeService() { Id = 31,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "اسباب کشی با وانت و نیسان",Price=2000,SubCategoryId=13 /*,ImagePath = "" */},
                new HomeService() { Id = 32,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کارگر جابه جایی",Price=2000,SubCategoryId=13 /*,ImagePath = "" */},
                new HomeService() { Id = 33,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعویض باتری خودرو",Price=2000,SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 34,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "باتری به باتری",Price=2000,SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 35,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "حمل خودرو",Price=2000,SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 36,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعویض وایر و شمع خودرو",Price=2000,SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 37,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "براشینگ موی بانوان",Price=2000,SubCategoryId=15 /*,ImagePath = "" */},
                new HomeService() { Id = 38,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کوتاهی موی بانوان",Price=2000,SubCategoryId=15 /*,ImagePath = "" */},
                new HomeService() { Id = 39,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "بافت موی بانوان در خانه",Price=2000,SubCategoryId=15 /*,ImagePath = "" */},
                new HomeService() { Id = 40,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "مراقبت و نگهداری",Price=2000,SubCategoryId=16 /*,ImagePath = "" */},
                new HomeService() { Id = 41,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "معاینه پزشکی",Price=2000,SubCategoryId=16 /*,ImagePath = "" */},
                new HomeService() { Id = 42,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "پیراپزشکی",Price=2000,SubCategoryId=16 /*,ImagePath = "" */},
                new HomeService() { Id = 43,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "پت شاپ",Price=2000,SubCategoryId=17 /*,ImagePath = "" */},
                new HomeService() { Id = 44,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "خدمات دامپزشکی در محل",Price=2000,SubCategoryId=17 /*,ImagePath = "" */},
                new HomeService() { Id = 45,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کلاس یوگا در خانه",Price=2000,SubCategoryId=18 /*,ImagePath = "" */},
                new HomeService() { Id = 46,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کلاس پیلاتس در خانه",Price=2000,SubCategoryId=18 /*,ImagePath = "" */},
                new HomeService() { Id = 47,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "پیشنهاد فروش خدمات آچاره به شرکت ها",Price=2000,SubCategoryId=19 /*,ImagePath = "" */},
                new HomeService() { Id = 48,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "استخدام نیروی خدمتکار",Price=2000,SubCategoryId=20 /*,ImagePath = "" */},
                new HomeService() { Id = 49,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیرات لباس",Price=2000,SubCategoryId=21 /*,ImagePath = "" */},
                new HomeService() { Id = 50,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "دوخت لباس زنانه",Price=2000,SubCategoryId=21 /*,ImagePath = "" */},
                new HomeService() { Id = 51,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "تعمیر کیف و کفش",Price=2000,SubCategoryId=21 /*,ImagePath = "" */},
                new HomeService() { Id = 52,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کیک و شیرینی",Price=2000,SubCategoryId=22 /*,ImagePath = "" */},
                new HomeService() { Id = 53,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "دکور تولد",Price=2000,SubCategoryId=22 /*,ImagePath = "" */},
                new HomeService() { Id = 54,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "گل آرایی",Price=2000,SubCategoryId=22 /*,ImagePath = "" */},
                new HomeService() { Id = 55,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "فینگرفود",Price=2000,SubCategoryId=22 /*,ImagePath = "" */},
                new HomeService() { Id = 56,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "آموزش زبان های خارجی",Price=2000,SubCategoryId=23 /*,ImagePath = "" */},
                new HomeService() { Id = 57,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "آموزش ابتدایی تا متوسطه",Price=2000,SubCategoryId=23 /*,ImagePath = "" */},
                new HomeService() { Id = 58,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "کوتاهی موی کودک",Price=2000,SubCategoryId=24 /*,ImagePath = "" */},
                new HomeService() { Id = 59,CreatedAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description="", Title = "طراحی و دیزاین اتاق کودک",Price=2000,SubCategoryId=24 /*,ImagePath = "" */},
            }); 





        }
    }
}
