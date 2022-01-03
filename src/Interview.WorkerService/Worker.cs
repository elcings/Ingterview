using EventBus.Base.Abstraction;
using Interview.Application.CarError.Command;
using Interview.Application.Distances.Command;
using Interview.Application.Fuel.Command;
using Interview.WorkerService.IntegrationEvents.EventHnadlers;
using Interview.WorkerService.IntegrationEvents.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IServiceProvider Services { get; }
        IEventBus _eventBus;

        public Worker(ILogger<Worker> logger, IServiceProvider services,IEventBus eventBus)
        {
            Services = services;
            _logger = logger;
            _eventBus = eventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                //_eventBus.Subscribe<CarStoppedCompletedIntegrationEvent, CarStoppedCompletedIntegrationEventHandler>();

                using (var scope = Services.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(new CreateErrorCommand { Description = "Your car have fault" });
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
