using Interview.Domain.AggregateModels.Orders;
using Interview.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Events
{
    public class OrderCancelledDomainEvent: INotification
    {
        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
