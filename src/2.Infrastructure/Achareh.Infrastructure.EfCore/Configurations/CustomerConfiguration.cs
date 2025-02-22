using Achareh.Domain.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
           .WithOne(x => x.Customer)
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Reviews)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasData
            (
                 new Customer
                 {
                     Id = 1,
                     UserId=2
                 }
                
            ); 



        }


    }
}
