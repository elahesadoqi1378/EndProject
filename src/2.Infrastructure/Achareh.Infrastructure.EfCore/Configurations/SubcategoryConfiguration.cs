
using Achareh.Domain.Core.Entities.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {

            builder.HasKey(x => x.Id);
            builder.ToTable("SubCategories");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);


            builder.HasOne(x => x.Category)
               .WithMany(x => x.SubCategories)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.HomeServices)
               .WithOne(x => x.SubCategory)
               .HasForeignKey(x => x.SubCategoryId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new List<SubCategory>()
            {
                new SubCategory() { Id = 1,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "نظافت و پذیرایی",CategoryId=1 , ImagePath = "/images/subcategory/nezafat_pazirayi.jpg"},
                new SubCategory() { Id = 2,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "شستشو",CategoryId=1 , ImagePath = "/images/subcategory/shostosho.jpg" },
                new SubCategory() { Id = 3,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "کارواش و دیتیلینگ",CategoryId=1 ,ImagePath = "/images/subcategory/karvash_detailing" },
                new SubCategory() { Id = 4,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "سرمایش و گرمایش",CategoryId=2 ,ImagePath = "/images/subcategory/sarmayesh_garmayesh" },
                new SubCategory() { Id = 5,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تعمیرات ساختمان",CategoryId=2 ,ImagePath = "/images/subcategory/tamirat_sakhteman" },
                new SubCategory() { Id = 6,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "لوله کشی",CategoryId=2 ,ImagePath = "/images/subcategory/lolekeshi" },
                new SubCategory() { Id = 7,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "طراحی و بازسازی ساختمان",CategoryId=2 ,ImagePath = "/images/subcategory/tarahi_bazsazi.jpg" },
                new SubCategory() { Id = 8,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "باغبانی و فضای سبز",CategoryId=2 ,ImagePath = "/images/subcategory/baqbani_fazayesabz.jpg"  },
                new SubCategory() { Id = 9,  CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "چوب و کابینت",CategoryId=2 ,ImagePath = "/images/subcategory/choob_kabinet.jpg"  },
                new SubCategory() { Id = 10, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "نصب و تعمیر لوازم خانگی",CategoryId=3 ,ImagePath = "/images/subcategory/nasab_tamir_lavazem.jpg" },
                new SubCategory() { Id = 11, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خدمات کامپیوتری",CategoryId=3 ,ImagePath = "/images/subcategory/khadamt_cp.jpg" },
                new SubCategory() { Id = 12, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تعمیرات موبایل",CategoryId=3 ,ImagePath = "/images/subcategory/tamirat_mobile.jpg" },
                new SubCategory() { Id = 13, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "باربری و جابجایی",CategoryId=4 ,ImagePath = "/images/subcategory/barbari_jabejayi.jpg" },
                new SubCategory() { Id = 14, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خدمات و تعمیرات خودرو",CategoryId=5 ,ImagePath = "/images/subcategory/khadamat_khodro.jpg" },
                new SubCategory() { Id = 15, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "زیبایی بانوان",CategoryId=6 ,ImagePath = "/images/subcategory/zibayi_banovan.jpg" },
                new SubCategory() { Id = 16, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "پزشکی و پرستاری",CategoryId=6 ,ImagePath = "/images/subcategory/pezeshki_parastari.jpg" },
                new SubCategory() { Id = 17, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "حیوانات خانگی",CategoryId=6 ,ImagePath = "/images/subcategory/heyvanat_khanegi.jpg" },
                new SubCategory() { Id = 18, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تندرستی و ورزش",CategoryId=6 ,ImagePath = "/images/subcategory/tandorosti_varzesh.jpg" },
                new SubCategory() { Id = 19, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خدمات شرکتی",CategoryId=7 ,ImagePath = "/images/subcategory/khadamat_sherkati.jpg" },
                new SubCategory() { Id = 20, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تامین نیروی انسانی",CategoryId=7 ,ImagePath = "/images/subcategory/tamin_niroye_ensani.jpg" },
                new SubCategory() { Id = 21, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خیاطی و تعمیرات لباس",CategoryId=8 ,ImagePath = "/images/subcategory/khayati_tamirat.jpg" },
                new SubCategory() { Id = 22, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "مجالس و رویدادها",CategoryId=8 ,ImagePath = "/images/subcategory/majales_roydad.jpg" },
                new SubCategory() { Id = 23, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "آموزش",CategoryId=8,ImagePath = "/images/subcategory/amozesh.jpg" },
                new SubCategory() { Id = 24, CreatedAt = new DateTime(2025,2,2), IsDeleted = false, Title = "کودک",CategoryId=8 ,ImagePath = "/images/subcategory/kodak.jpg" },
            });


        }
    }
}
