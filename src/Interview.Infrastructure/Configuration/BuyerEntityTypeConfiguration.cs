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
    class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> buyerConfiguration)
        {
            buyerConfiguration.ToTable("buyers", OrderDbContext.DEFAULT_SCHEMA);

            buyerConfiguration.HasKey(b => b.Id);

            buyerConfiguration.Ignore(b => b.DomainEvents);

            buyerConfiguration.Property(b => b.Id).ValueGeneratedOnAdd();

            buyerConfiguration.Property(b => b.IdentityGuid)
                .HasMaxLength(200)
                .IsRequired();

            buyerConfiguration.HasIndex("IdentityGuid")
                .IsUnique(true);

            buyerConfiguration.Property(b => b.Name);

            buyerConfiguration.HasMany(b => b.PaymentMethods)
                .WithOne()
                .HasForeignKey("BuyerId")
                .OnDelete(DeleteBehavior.Cascade);

            var navigation = buyerConfiguration.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
