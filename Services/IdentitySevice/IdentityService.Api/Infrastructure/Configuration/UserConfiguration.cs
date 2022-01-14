using IdentityService.Api.Core.Domain.Entities;
using IdentityService.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> buyerConfiguration)
        {
            buyerConfiguration.ToTable("users", IdentityContext.DEFAULT_SCHEMA);

            buyerConfiguration.HasKey(b => b.Id);
            buyerConfiguration.Property(b => b.Id).ValueGeneratedOnAdd();
            buyerConfiguration.Property(x => x.Email).IsRequired();
            buyerConfiguration.HasMany(x => x.Roles).WithMany(x => x.Users).UsingEntity(join => join.ToTable("UserRole"));
            buyerConfiguration.Property(x => x.Firstname).IsRequired().HasColumnName("FirstName");
            buyerConfiguration.Property(x => x.Lastname).IsRequired().HasColumnName("LastName");
            buyerConfiguration.Property(x => x.Created).IsRequired().HasDefaultValue(DateTime.Now);
            buyerConfiguration.Property(x => x.Email).IsRequired();
        }
    }
}
