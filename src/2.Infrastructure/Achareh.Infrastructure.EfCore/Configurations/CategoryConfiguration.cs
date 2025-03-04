
using Achareh.Domain.Core.Entities.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Categories");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.SubCategories)
              .WithOne(x => x.Category)
              .HasForeignKey(x => x.CategoryId)
              .OnDelete(DeleteBehavior.NoAction);


            builder.HasData(new List<Category>()
            {
                new Category() { Id = 1, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "تمیزکاری", ImagePath = "/images/category/tamizkari.jpg"},
                new Category() { Id = 2, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "ساختمان", ImagePath = "/images/category/sakhteman.jpg" },
                new Category() { Id = 3, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "تعمیرات اشیا",ImagePath = "/images/category/tamirat_ashya.jpg" },
                new Category() { Id = 4, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "اسباب کشی و حمل بار",ImagePath = "/images/category/asbabkeshi.jpg" },
                new Category() { Id = 5, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "خودرو",ImagePath = "/images/category/khodro.jpg" },
                new Category() { Id = 6, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "سلامت و زیبایی",ImagePath = "/images/category/salamat_zibayi.jpg" },
                new Category() { Id = 7, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "سازمان ها و مجتمع ها",ImagePath = "/images/category/sazmanha_va_mojtamha.jpg" },
                new Category() { Id = 8, CreatedAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "سایر",ImagePath = "/images/category/sayer.jpg" },


            });

        }
    }
}
