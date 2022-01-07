using Interview.Application.CarError.Command;
using Interview.Application.Common.Interfaces;
using Interview.Application.Distances.Command;
using Interview.Application.Fuel.Command;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        ILogger<DoWork> _logger;
        public DoWork(IExternalClientService externalClientService , IServiceProvider services,ILogger<DoWork> logger)
        {
            Services = services;
            _externalClientService = externalClientService;
            _logger = logger;
         
        }

        public async Task RunAsync()
        {
            var error = await _externalClientService.GetDinOfficesAsync();
            using (var scope = Services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                if (string.IsNullOrEmpty(error.Message))
                {
                    await mediator.Send(new CreateErrorCommand { Description = JsonConvert.SerializeObject(error) });
                }
                else {
                    _logger.LogError("DoWork.RunAsync:{0}",error.Message);
                
                }

            }
        }
    }
}
