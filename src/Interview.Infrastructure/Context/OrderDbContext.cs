using Interview.Application.Common.Interfaces;
using Interview.Domain.AggregateModels.Buyer;
using Interview.Domain.AggregateModels.Orders;
using Interview.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Infrastructure
{
   public class OrderDbContext:DbContext, ICarDbContext,IUnitOfWork
    {
        private readonly IDomainEventService _domainEventService;
        private IMediator _mediator;

        public const string DEFAULT_SCHEMA = "ordering";
        public OrderDbContext(DbContextOptions<OrderDbContext> options,IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Order> Orders  => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<PaymentMethod> Payments =>Set<PaymentMethod>();
        public DbSet<Buyer> Buyers => Set<Buyer>();
        public DbSet<CardType> CardTypes => Set<CardType>();
        public DbSet<OrderStatus> OrderStatus => Set<OrderStatus>();

     

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = "Service";
        //                entry.Entity.Created = DateTime.Now;
        //                break;

        //            case EntityState.Modified:
        //                entry.Entity.LastModified = DateTime.Now;
        //                break;
        //        }
        //    }
        // //   await _mediator.DispatchDomainEventsAsync(this);

        //    //var events = ChangeTracker.Entries<IHasDomainEvent>()
        //    //        .Select(x => x.Entity.DomainEvents)
        //    //        .SelectMany(x => x)
        //    //        .Where(domainEvent => !domainEvent.IsPublished)
        //    //        .ToArray();

        //    var result = await base.SaveChangesAsync(cancellationToken);


        //    return result;
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Service";
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            await _mediator.DispatchDomainEventsAsync(this);

            

            var result = await base.SaveChangesAsync(cancellationToken);


            return true;
        }
        //private async Task DispatchEvents(DomainEvent[] events)
        //{
        //    foreach (var @event in events)
        //    {
        //        @event.IsPublished = true;
        //        await _domainEventService.Publish(@event);
        //    }
        //}


    }
}
