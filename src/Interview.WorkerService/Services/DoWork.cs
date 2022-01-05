using Interview.Application.CarError.Command;
using Interview.Application.Common.Interfaces;
using Interview.Application.Distances.Command;
using Interview.Application.Fuel.Command;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.WorkerService.Services
{
    public class DoWork: IDoWork
    {
        private IExternalClientService _externalClientService;
        IServiceProvider Services { get; }
        public DoWork(IExternalClientService externalClientService , IServiceProvider services)
        {
            Services = services;
            _externalClientService = externalClientService;
        }

        public async Task RunAsync()
        {
            var fuel = await _externalClientService.GetFuelLevelAsync();
            var distance = await _externalClientService.GetTravelDistanceAsync();
            var error = await _externalClientService.GetErrorAsync();
           // var error = "Error in car";
            using (var scope = Services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                if (!string.IsNullOrEmpty(error))
                {
                    await mediator.Send(new CreateErrorCommand { Description = error});
                }
                await mediator.Send(new CreateDistanceCommand { Distance = distance });
                await mediator.Send(new CreateFuelLevelCommand { Level = fuel });
            }
        }
    }
}
