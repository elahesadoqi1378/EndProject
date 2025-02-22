using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Achareh.Infrastructure.EfCore.Configurations;
using Achareh.Domain.Core.Entities.BaseEntities;

namespace Achareh.Infrastructure.EfCore.Common
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.ApplyConfiguration(new AdminConfiguration());
            builder.ApplyConfiguration(new RequestConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new ExpertConfiguration());
            builder.ApplyConfiguration(new HomeServiceConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new SubcategoryConfiguration());
            builder.ApplyConfiguration(new ExpertOfferConfiguration());
            builder.ApplyConfiguration(new ImageConfigurations());

            UserConfiguration.SeedUsers(builder);


            base.OnModelCreating(builder);


        }

 
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseSqlServer
            //       ("Data Source =ELAMIR\\SQLEXPRESS; Database = Achareh; Integrated Security=True; User ID = sa; Password =123456 ; TrustServerCertificate=True");
            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ExpertOffer> ExpertOffers { get; set; }
        public DbSet<HomeService> HomeServices { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Admin> Admins { get; set; }

    }
}
