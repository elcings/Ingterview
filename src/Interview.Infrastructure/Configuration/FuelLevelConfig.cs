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
    class FuelLevelConfig:IEntityTypeConfiguration<FuelLevel>
    {
        public void Configure(EntityTypeBuilder<FuelLevel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Created).IsRequired().HasColumnName("Date");
            builder.Property(x => x.Level).IsRequired();
        }
    }
}
