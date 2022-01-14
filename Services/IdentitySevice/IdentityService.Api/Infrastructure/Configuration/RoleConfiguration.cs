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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> buyerConfiguration)
        {
            buyerConfiguration.ToTable("roles", IdentityContext.DEFAULT_SCHEMA);

            buyerConfiguration.HasKey(b => b.Id);
            buyerConfiguration.Property(b => b.Id).ValueGeneratedOnAdd();
            buyerConfiguration.Property(x => x.Name).IsRequired(true);
            buyerConfiguration.Property(x => x.Created).IsRequired(true).HasDefaultValue(DateTime.Now);
        }
    }
}
