using EventBus.Base.Abstraction;
using Interview.Application.CarError.Command;
using Interview.Application.Distances.Command;
using Interview.Application.Fuel.Command;
using Interview.WorkerService.IntegrationEvents.EventHnadlers;
using Interview.WorkerService.IntegrationEvents.Events;
using Interview.WorkerService.Services;
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
        IDoWork _doWork;

        public Worker(ILogger<Worker> logger, IServiceProvider services,IEventBus eventBus,IDoWork doWork)
        {
            Services = services;
            _logger = logger;
            _eventBus = eventBus;
            _doWork = doWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                //  _eventBus.Subscribe<CarStoppedCompletedIntegrationEvent, CarStoppedCompletedIntegrationEventHandler>();

                //using (var scope = Services.CreateScope())
                //{
                //    var dowork = scope.ServiceProvider.GetRequiredService<IDoWork>();
                    await  _doWork.RunAsync();
                  
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
               // }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
