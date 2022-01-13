using Interview.Domain.AggregateModels.Buyer;
using Interview.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Events
{
    public class BuyerAndPaymentMethodVerifiedDomainEvent: INotification
    {
        public Buyer Buyer { get; private set; }
        public PaymentMethod Payment { get; private set; }
        public Guid OrderId { get; private set; }

        public BuyerAndPaymentMethodVerifiedDomainEvent(Buyer buyer, PaymentMethod payment, Guid orderId)
        {
            Buyer = buyer;
            Payment = payment;
            OrderId = orderId;
        }
    }
}
