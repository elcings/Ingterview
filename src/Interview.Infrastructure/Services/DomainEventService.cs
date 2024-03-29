﻿using Interview.Application.Common.Interfaces;
using Interview.Application.Common.Models;
using Interview.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Services
{
    public class DomainEventService: IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IPublisher _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public  Task Publish(DomainEvent domainEvent)
        {
            //_logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            //await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
            return Task.CompletedTask;
        }

        //private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        //{
        //    return (INotification)Activator.CreateInstance(
        //        typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
        //}
    }
}
