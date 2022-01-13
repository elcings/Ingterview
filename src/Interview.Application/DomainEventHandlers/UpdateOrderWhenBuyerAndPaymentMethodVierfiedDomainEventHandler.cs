using Interview.Domain.Events;
using Interview.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.DomainEventHandlers
{
    public class UpdateOrderWhenBuyerAndPaymentMethodVierfiedDomainEventHandler : INotificationHandler<BuyerAndPaymentMethodVerifiedDomainEvent>
    {
        private readonly IOrderRepository _repository;
        public UpdateOrderWhenBuyerAndPaymentMethodVierfiedDomainEventHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(BuyerAndPaymentMethodVerifiedDomainEvent notification, CancellationToken cancellationToken)
        {
            var order =await _repository.GetByIdFilter(notification.OrderId, "OrderItems,OrderStatus");
            order.SetBuyerId(notification.Buyer.Id);
            order.SetPaymentId(notification.Payment.Id);
        }
    }
}
