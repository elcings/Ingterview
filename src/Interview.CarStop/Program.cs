using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Interview.Application;
using Interview.Application.CarError.Queries;
using Interview.CarStop.Events;
using Interview.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Interview.CarStop
{
    class Program
    {
        static void Main(string[] args)
        {


            while (true)
            {
                Console.WriteLine("Please if you want to stop car write stop");
                var command = Console.ReadLine();
                if (command.ToLower() == "stop")
                {
                    ServiceCollection services = new ServiceCollection();
                    ConfigureServices(services);

                    var sp = services.BuildServiceProvider();
                    IEventBus eventBus = sp.GetRequiredService<IEventBus>();

                    eventBus.Publis(new CarStoppedCompletedIntegrationEvent(command));
                }
                if (command == "exit")
                {
                    break;
                }
                else if (command == "clear")
                {
                    Console.Clear();
                }
                Console.WriteLine("Car Started...");
            }
            Console.WriteLine("Service Stopped...");


        }
       

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(con => {
                con.AddConsole();
            });
           // IConfiguration configuration = hostContext.Configuration;
            services.AddApplication();
          //  services.AddInfrastructure(configuration);
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 3,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "CarStopService",
                    EventBusType = EventBusType.RabbitMQ

                };
                return EventBusFactory.Create(config, sp);

            });
        }
    }
}
