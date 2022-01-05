using Interview.Domain.Repositories;
using Interview.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Interview.Application.Common.Interfaces;
using Interview.Infrastructure.Services;
using Interview.Application.Common.Models;
using System;

namespace Interview.Infrastructure
{
    public static class DependencyInjection
    {
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
                
            services.AddHttpClient<IExternalClientService, ExternalClientService>();
            return services;
        }
    }
}
