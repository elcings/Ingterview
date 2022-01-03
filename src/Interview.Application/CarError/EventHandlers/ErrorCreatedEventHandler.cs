using Interview.Application.Common.Interfaces;
using Interview.Application.Common.Models;
using Interview.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.CarError.EventHandlers
{
    public class ErrorCreatedEventHandler : INotificationHandler<DomainEventNotification<ErrorCreatedEvent>>
    {
        private readonly ILogger<ErrorCreatedEventHandler> _logger;
        private IMailService _mailService;

        public ErrorCreatedEventHandler(ILogger<ErrorCreatedEventHandler> logger, IMailService mailService)
        {
            _mailService = mailService;
            _logger = logger;
        }
        
        public async  Task Handle(DomainEventNotification<ErrorCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);
           await _mailService.SendEmail(new MailRequest { ToEmail = "elcinaliyevgs@gmail.com", Body = domainEvent.Error.Description,Subject="Fault in your car" });
        }
    }
}
