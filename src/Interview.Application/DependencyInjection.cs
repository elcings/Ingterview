using FluentValidation;
using Interview.Application.CarError.Command;
using Interview.Application.Common.Behaviours;
using Interview.Application.Common.Mapping;
using Interview.Application.Distances.Command;
using Interview.Application.Fuel.Command;
using MediatR;
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
            services.AddMediatR(Assembly.GetExecutingAssembly());
                services.AddMediatR(typeof(CreateFuelLevelCommand).GetTypeInfo().Assembly);
                services.AddMediatR(typeof(CreateDistanceCommand).GetTypeInfo().Assembly);
                services.AddMediatR(typeof(CreateErrorCommand).GetTypeInfo().Assembly);
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

                return services;
            }
        }
    
}
