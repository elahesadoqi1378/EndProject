using Achareh.Domain.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
            .WithOne(x => x.Admin)
            .OnDelete(DeleteBehavior.Cascade); //noaction

            builder.HasData
           (
                new Admin
                {
                    Id = 1,
                    UserId = 1

                }

           );
        }
    }
}
