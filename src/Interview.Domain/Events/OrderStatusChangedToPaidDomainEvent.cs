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
    public class OrderStatusChangedToPaidDomainEvent: INotification
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderItem> OrderItems { get; }

        public OrderStatusChangedToPaidDomainEvent(Guid orderId,
            IEnumerable<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}
