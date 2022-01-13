using Interview.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Events
{
    public class OrderStatusChangedToStockConfirmedDomainEvent: INotification
    {
        public Guid OrderId { get; }

        public OrderStatusChangedToStockConfirmedDomainEvent(Guid orderId)
            => OrderId = orderId;
    }
}
