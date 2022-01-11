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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarDbContext>(option => 
                option.UseSqlServer(configuration.GetConnectionString("DefaultDBConnection"), sqlServerOptionsAction: b =>
                {
                    b.EnableRetryOnFailure(maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(3),
                    errorNumbersToAdd: null);
                }).EnableSensitiveDataLogging());



            services.AddScoped<ICarDbContext>(provider => provider.GetRequiredService<CarDbContext>());



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
