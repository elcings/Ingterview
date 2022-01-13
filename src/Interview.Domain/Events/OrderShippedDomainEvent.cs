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
    class OrderShippedDomainEvent: INotification
    {
        public Order Order { get; }

        public OrderShippedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
