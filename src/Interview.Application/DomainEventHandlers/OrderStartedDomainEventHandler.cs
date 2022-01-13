using Interview.Domain.AggregateModels.Buyer;
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
    public class OrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        IBuyerRepository _repository;
        public OrderStartedDomainEventHandler(IBuyerRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(OrderStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            var cardTypeId = (notification.CardTypeId != 0) ? notification.CardTypeId : 1;


            var buyer = (await _repository.Query(x => x.Name == notification.UserName, includeProperties: "PaymentMethods")).FirstOrDefault();
            bool buyerOriginallyExist = buyer != null;

            if (!buyerOriginallyExist)
            {
                buyer = new Buyer(Guid.NewGuid().ToString(), notification.UserName);
            }

            buyer.VerifyOrAddPaymentMethod(cardTypeId,
                 $"Payment Method on {DateTime.Now}",notification.CardNumber,
                 notification.CardSecurityNumber,
                 notification.CardHolderName,
                 notification.CardExpiration,
                 notification.Order.Id);

            var buyerUpdated = buyerOriginallyExist ? await _repository.Update(buyer) : await _repository.Create(buyer);
           await  _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

           
        }
    }
}
