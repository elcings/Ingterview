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
    public class ErrorConfig : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Created).IsRequired().HasColumnName("Date");
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
