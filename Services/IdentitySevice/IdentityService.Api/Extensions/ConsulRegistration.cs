using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Extensions
{
    public static class ConsulRegistration
    {
        public static IServiceCollection ConfigureConsul(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
            {
                var address = configuration["ConsulConfig:Address"];
                config.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {

            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger<IApplicationBuilder>();


            var features = app.ServerFeatures;
            var addresses = features.Get<IServerAddressesFeature>();
            var address = addresses.Addresses.First();

            var uri = new Uri(address);

            var registration = new AgentServiceRegistration() {
            ID="IdentityService",
            Name= "IdentityService",
            Address=$"{uri.Host}",
            Port=uri.Port,
                Tags = new []{"Identity Service","Identity" }
            };
            logger.LogInformation("Registration with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregistration from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });
            return app;
        }
    }
}
