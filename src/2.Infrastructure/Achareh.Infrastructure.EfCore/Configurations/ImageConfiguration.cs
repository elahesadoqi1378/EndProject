using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achareh.Domain.Core.Entities.BaseEntities;

namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class ImageConfigurations : IEntityTypeConfiguration<Image>

    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Images");
            builder.Property(x => x.Path).HasMaxLength(500).IsRequired();

            builder.HasOne(x => x.Request)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.RequestId)
                .OnDelete(DeleteBehavior.NoAction);

            
        }
    }
}
