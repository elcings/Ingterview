using BasketService.Api.Core.Domain;
using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Api.IntegrationEvents.Events
{
    public class OrderCreatedIntegraionEvent:IntegrationEvent
    {
        public OrderCreatedIntegraionEvent(string userId, string userName,  string city, string street, 
            string state, string country, string zipCode, string cardNumber, string cardHolderName, 
            DateTime cardExpr, string cardSecurityNumber, int cardTypeId, string buyer, CustomerBasket basket, Guid requestId)
        {
            UserId = userId;
            UserName = userName;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipCode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpr = cardExpr;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            Buyer = buyer;
            RequestId = requestId;
            Basket = basket;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public int OrderNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpr { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardTypeId { get; set; }
        public string Buyer { get; set; }
        public CustomerBasket Basket { get; set; }
        public Guid RequestId { get; set; }
    }
}
