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
            services.AddDbContext<OrderDbContext>(option => 
                option.UseSqlServer(configuration.GetConnectionString("DefaultDBConnection"), sqlServerOptionsAction: b =>
                {
                    b.EnableRetryOnFailure(maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(3),
                    errorNumbersToAdd: null);
                }).EnableSensitiveDataLogging());



            services.AddScoped<ICarDbContext>(provider => provider.GetRequiredService<OrderDbContext>());



            services.AddOptions();
            MailSettings mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
            services.AddSingleton(mailSettings);
            ExternalSettings externalSettings = configuration.GetSection("ExternalSettings").Get<ExternalSettings>();
            services.AddSingleton(externalSettings);
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddScoped(typeof(IBuyerRepository), typeof(BuyerRepository));
            //services.AddScoped(typeof(IDomainEventService), typeof(DomainEventService));

            services.AddScoped<IMailService, MailService>();
                
            services.AddHttpClient<IExternalClientService, ExternalClientService>(c=> {
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{externalSettings.Token}");
            });

            //var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>()
            //    .UseSqlServer(configuration.GetConnectionString("DefaultDBConnection"));

            //var dbContext = new OrderDbContext(optionsBuilder.Options, null);
            //dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();
            return services;
        }
    }
}
