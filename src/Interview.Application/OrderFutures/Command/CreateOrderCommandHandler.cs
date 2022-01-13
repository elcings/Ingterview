using Ardalis.GuardClauses;
using EventBus.Base.Abstraction;
using Interview.Application.Common;
using Interview.Application.Common.Models;
using Interview.Application.IntegrationEvents;
using Interview.Application.Validations;
using Interview.Domain.AggregateModels.Orders;
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
    public class Validator : IValidationHandler<CreateOrderCommand>
    {
        public async Task<ValidationResult> Validate(CreateOrderCommand request)
        {
            ValidationResult result;
            if (Guard.Against.IsNullOrEmpty(request.CardNumber, nameof(request.CardNumber), message: "CardNumber must not be null or empty", out result))
            {
                return result;
            }

            return ValidationResult.Success;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ActionResult>
    {
        private readonly IOrderRepository _repository;
        private IEventBus _eventBus;

        public CreateOrderCommandHandler(IOrderRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<ActionResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var addr = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);

            Order order = new Order("", request.UserName, addr, request.CardTypeId, request.CardNumber, request.CardSecurityNumber, request.CardholderName, request.CardExpr);
            request.OrderItemDtos.ForEach(x => order.AddOrderItem(x.ProductId, x.ProductName, x.UnitPrice, 0, x.PictureUrl, x.Unit));
            var isSave = await _repository.Create(order);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserName);
            _eventBus.Publis(orderStartedIntegrationEvent);
            return new ActionResult().Succeed();

        }
    }
}
