using BasketService.Api.Core.Application.Repositories;
using BasketService.Api.Core.Application.Services;
using BasketService.Api.Core.Infastructure.Repositories;
using BasketService.Api.Extensions;
using BasketService.Api.IntegrationEvents.EventHandlers;
using BasketService.Api.IntegrationEvents.Events;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton(s => s.ConfigureRedis(Configuration));
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 3,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "BasketService",
                    EventBusType = EventBusType.RabbitMQ

                };
                return EventBusFactory.Create(config, sp);

            });
            services.AddTransient<OrderCreatedIntegrationEventHandler>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderCreatedIntegraionEvent, OrderCreatedIntegrationEventHandler>();
        }
    }
}