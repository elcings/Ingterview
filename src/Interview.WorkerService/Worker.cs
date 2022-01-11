using EventBus.Base.Abstraction;
using Interview.Application.CarError.Command;
using Interview.Application.CarError.Queries;
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
                _logger.LogInformation("Application started  at: {time}", DateTimeOffset.Now);

                //  _eventBus.Subscribe<CarStoppedCompletedIntegrationEvent, CarStoppedCompletedIntegrationEventHandler>();

                using (var scope = Services.CreateScope())
                {
                    //  var dowork = scope.ServiceProvider.GetRequiredService<IDoWork>();
                    //  await dowork.RunAsync();
                    var mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();
                      //var Id=  await mediatr.Send(new CreateDistanceCommand() { Distance = 45,Colour= "#FF5733" ,ToDoItems=new List<Application.Common.Models.ToDoItemDTO> { new Application.Common.Models.ToDoItemDTO { Name="Elcin"},new Application.Common.Models.ToDoItemDTO { Name="Ilkin"} } });

                     // var id = await mediatr.Send(new CreateErrorCommand() { Description="test"});
                      var result = mediatr.Send(new GetErrorByIdQuery() );
                    //{ Id = Guid.Parse("46A65ADF-B9BF-4821-A8F3-5EB0101DAC6D") }
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await Task.Delay(1000, stoppingToken);
               
            }
            _logger.LogInformation("Application Stopped  at: {time}", DateTimeOffset.Now);
        }
    }
}
