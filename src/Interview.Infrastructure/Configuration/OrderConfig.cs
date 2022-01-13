using Interview.Domain.AggregateModels.Buyer;
using Interview.Domain.AggregateModels.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.ToTable("orders", OrderDbContext.DEFAULT_SCHEMA);

            orderConfiguration.HasKey(o => o.Id);

            orderConfiguration.Ignore(b => b.DomainEvents);

            orderConfiguration.Property(o => o.Id).ValueGeneratedOnAdd();

            //Address value object persisted as owned entity type supported since EF Core 2.0
            orderConfiguration
                .OwnsOne(o => o.Address, a =>
                {
                    a.WithOwner();
                });

            orderConfiguration
                .Property<Guid?>("_buyerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BuyerId")
                .IsRequired(false);

            orderConfiguration
                .Property<DateTime>("_orderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();

            orderConfiguration
                .Property<int>("_orderStatusId")
                // .HasField("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();

            orderConfiguration
                .Property<Guid?>("_paymentMethodId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentMethodId")
                .IsRequired(false);

            orderConfiguration.Property<string>("Description").IsRequired(false);

            var navigation = orderConfiguration.Metadata.FindNavigation(nameof(Order.OrderItems));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            orderConfiguration.HasOne<PaymentMethod>()
                .WithMany()
                // .HasForeignKey("PaymentMethodId")
                .HasForeignKey("_paymentMethodId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            orderConfiguration.HasOne<Buyer>()
                .WithMany()
                .IsRequired(false)
                // .HasForeignKey("BuyerId");
                .HasForeignKey("_buyerId");

            orderConfiguration.HasOne(o => o.OrderStatus)
                .WithMany()
                // .HasForeignKey("OrderStatusId");
                .HasForeignKey("_orderStatusId");
        }
    }
}
