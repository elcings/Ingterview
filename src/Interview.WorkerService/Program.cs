using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Interview.Application;
using Interview.Infrastructure;
using Interview.WorkerService.IntegrationEvents.EventHnadlers;
using Interview.WorkerService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Interview.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddApplication();
                    services.AddInfrastructure(configuration);
                    services.AddScoped<CarStoppedCompletedIntegrationEventHandler>();
                    services.AddSingleton<IEventBus>(sp =>
                    {
                        EventBusConfig config = new()
                        {
                            ConnectionRetryCount = 3,
                            EventNameSuffix = "IntegrationEvent",
                            SubscriberClientAppName = "CarWorkerService",
                            EventBusType = EventBusType.RabbitMQ

                        };
                        return EventBusFactory.Create(config, sp);

                    });
                    services.AddScoped<IDoWork, DoWork>();
                    services.AddHostedService<Worker>();
                });
    }
}
