using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using FluentValidation;
using Interview.Application.Common.Behaviours;
using Interview.Application.Common.Extensions;
using Interview.Application.Common.Mapping;
using Interview.Application.Validations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
          
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddMemoryCache();
            services.AddValidators();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
         //   services.AddScoped<CarStartedIntegrationEventHandler>();
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 3,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "OrderService",
                    EventBusType = EventBusType.RabbitMQ

                };
                return EventBusFactory.Create(config, sp);

            });

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.Scan(scan =>
               scan.FromAssemblyOf<IValidationHandler>()
               .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
               .AsImplementedInterfaces().WithTransientLifetime());
            return services;
        }
    }
    
}
