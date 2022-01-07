using Interview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Configuration
{
    public class DistanceConfig : IEntityTypeConfiguration<Distance>
    {
        public void Configure(EntityTypeBuilder<Distance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Created).IsRequired().HasColumnName("Date");
            builder.Property(x => x.distance).IsRequired().HasColumnName("Distance");
            builder.OwnsOne(b => b.Colour);

        }
    }
}
