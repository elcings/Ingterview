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
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Interview.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {

      //      var configuration = new ConfigurationBuilder()
      //.SetBasePath(Directory.GetCurrentDirectory())
      //.AddJsonFile("appsettings.json")
      //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
      //.Build();

      //      Log.Logger = new LoggerConfiguration()
      //         .ReadFrom.Configuration(configuration)
      //         .CreateLogger();
            var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Application started....");
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                logger.Error(ex, "Exception duiring execution...");
                throw;
            }
            finally {
                LogManager.Shutdown();
            }
           
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
                })
            .ConfigureLogging(logging=> {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            
            }).UseNLog();
    }
}
