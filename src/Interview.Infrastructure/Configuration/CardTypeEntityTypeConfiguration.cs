using Interview.Domain.AggregateModels.Buyer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Configuration
{
    public class CardTypeEntityTypeConfiguration : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> cardTypesConfiguration)
        {
            cardTypesConfiguration.ToTable("cardtypes", OrderDbContext.DEFAULT_SCHEMA);

            cardTypesConfiguration.HasKey(ct => ct.Id);

            cardTypesConfiguration.Property(ct => ct.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            cardTypesConfiguration.Property(ct => ct.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
