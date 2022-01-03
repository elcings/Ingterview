using EventBus.Base.Abstraction;
using Interview.WorkerService.IntegrationEvents.Events;
using Interview.WorkerService.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.WorkerService.IntegrationEvents.EventHnadlers
{
    public class CarStoppedCompletedIntegrationEventHandler : IIntegrationEventHandler<CarStoppedCompletedIntegrationEvent>
    {
        private IDoWork _doWork;
        public CarStoppedCompletedIntegrationEventHandler(IDoWork doWork) 
        {
            _doWork = doWork;
        }
        public async Task Handle(CarStoppedCompletedIntegrationEvent @event)
        {
            if (@event.Message == "stop")
            {
               await _doWork.RunAsync();
            }
        }
    }
}
