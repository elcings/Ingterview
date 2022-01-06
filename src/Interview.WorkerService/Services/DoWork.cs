using Interview.Application.CarError.Command;
using Interview.Application.Common.Interfaces;
using Interview.Application.Distances.Command;
using Interview.Application.Fuel.Command;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
            var error = await _externalClientService.GetDinOfficesAsync();
           // var error = "Error in car";
            using (var scope = Services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                if (!string.IsNullOrEmpty(error.Message))
                {
                    await mediator.Send(new CreateErrorCommand { Description = JsonConvert.SerializeObject(error)});
                }

            }
        }
    }
}
