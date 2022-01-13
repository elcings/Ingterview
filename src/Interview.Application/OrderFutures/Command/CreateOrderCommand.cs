using Ardalis.GuardClauses;
using AutoMapper;
using EventBus.Base.Abstraction;
using Interview.Application.Common;
using Interview.Application.Common.Models;
using Interview.Application.Common.Models.Dtos;
using Interview.Application.IntegrationEvents;
using Interview.Application.Validations;
using Interview.Domain.AggregateModels.Orders;
using Interview.Domain.Models;
using Interview.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.OrderFutures.Command
{
    public class CreateOrderCommand:IRequest<ActionResult>
    {
        private readonly List<OrderItemDto> _orderItemDtos;
        public string UserName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardExpr { get; set; }
        public string CardSecurityNumber { get; set; }
        public string CardholderName { get; set; }
        public int CardTypeId { get; set; }
        public  List<OrderItemDto> OrderItemDtos=> _orderItemDtos;
        public CreateOrderCommand()
        {
            _orderItemDtos = new List<OrderItemDto>();
        }

        public CreateOrderCommand(List<BasketItem> basketItems,string username,string city,string state,string country,string street,string zipcode,
            string cardnumber,string cardholdername,   DateTime cardexpr,string cardSecurityNumber,int cardTypeId):base()
        {
            var list = basketItems.Select(item => new OrderItemDto
            {
                ProductId=item.ProductId ,
                PictureUrl=item.PictureUrl,
                ProductName = item.ProductName,
                Unit=item.Quantity,
                UnitPrice= (decimal)item.UnitPrice
                

            });
            _orderItemDtos = list.ToList();
            UserName = username;
            City = city;
            State = state;
            Country = country;
            Street = street;
            ZipCode = zipcode;
            CardNumber = cardnumber;
            CardExpr = cardexpr;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            CardholderName = cardholdername;
            
        }
    }
    //public class Validator : IValidationHandler<CreateOrderCommand>
    //{
    //    public async Task<ValidationResult> Validate(CreateOrderCommand request)
    //    {
    //        ValidationResult result;
    //        if (Guard.Against.IsNullOrEmpty(request.CardNumber, nameof(request.CardNumber), message: "CardNumber must not be null or empty", out result))
    //        {
    //            return result;
    //        }
           
    //        return ValidationResult.Success;
    //    }
    //}

    //public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ActionResult>
    //{
    //    private readonly IOrderRepository _repository;
    //    private IEventBus _eventBus;

    //    public CreateOrderCommandHandler(IOrderRepository repository, IEventBus eventBus)
    //    {
    //        _repository = repository;
    //        _eventBus = eventBus;
    //    }

    //    public async Task<ActionResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    //    {
    //        var addr = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);

    //        Order order = new Order("", request.UserName, addr, request.CardTypeId, request.CardNumber, request.CardSecurityNumber, request.CardholderName, request.CardExpr);
    //        request.OrderItemDtos.ForEach(x => order.AddOrderItem(x.ProductId, x.ProductName, x.UnitPrice, 0, x.PictureUrl, x.Unit));
    //        var entity=   await _repository.Create(order);
    //        var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserName);
    //        _eventBus.Publis(orderStartedIntegrationEvent);
    //        return new ActionResult().Succeed();

    //    }
    //}
}
