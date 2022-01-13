using Interview.Domain.AggregateModels.Buyer;
using Interview.Domain.AggregateModels.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.Common.Interfaces
{
    public interface ICarDbContext
    {
         DbSet<Order> Orders { get; }
         DbSet<OrderItem> OrderItems { get;  }
         DbSet<PaymentMethod> Payments { get;  }
         DbSet<Buyer> Buyers { get; }
         DbSet<CardType> CardTypes { get;  }
         DbSet<OrderStatus> OrderStatus { get;  }

       // Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
