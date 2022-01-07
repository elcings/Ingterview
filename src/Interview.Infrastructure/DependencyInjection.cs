using Interview.Domain.Repositories;
using Interview.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Interview.Application.Common.Interfaces;
using Interview.Infrastructure.Services;
using Interview.Application.Common.Models;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;

namespace Interview.Infrastructure
{
    public static class DependencyInjection
    {
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              // Handle HttpRequestExceptions, 408 and 5xx status codes
              .HandleTransientHttpError()
              // Handle 404 not found
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              // Handle 401 Unauthorized
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
              // What to do if any of the above erros occur:
              // Retry 3 times, each time wait 1,2 and 4 seconds before retrying.
              .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var t = configuration.GetConnectionString("DefaultDBConnection");
            services.AddDbContext<CarDbContext>(option => {
                option.UseSqlServer(configuration.GetConnectionString("DefaultDBConnection"));
            });
            services.AddOptions();
            MailSettings mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
            services.AddSingleton(mailSettings);
            ExternalSettings externalSettings = configuration.GetSection("ExternalSettings").Get<ExternalSettings>();
            services.AddSingleton(externalSettings);
            services.AddScoped(typeof(IDistanceRepository), typeof(DistanceRepository));
            services.AddScoped(typeof(IFuelLevelRepository), typeof(FuelLevelRepository));
            services.AddScoped(typeof(IErrorRepository), typeof(ErrorRepository));
            services.AddScoped(typeof(IDomainEventService), typeof(DomainEventService));

            services.AddScoped<IMailService, MailService>();
                
            services.AddHttpClient<IExternalClientService, ExternalClientService>(c=> {
                c.BaseAddress = new Uri(externalSettings.BaseAddress);
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{externalSettings.Token}");

            });
            return services;
        }
    }
}
