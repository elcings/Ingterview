using IdentityService.Api.Core.Application.Common;
using IdentityService.Api.Core.Application.Service;
using IdentityService.Api.Core.Domain.Repositories;
using IdentityService.Api.Infrastructure.Data;
using IdentityService.Api.Infrastructure.Repositories;
using IdentityService.Api.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure
{
    public static class DenependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(option => {
                option.UseSqlServer(configuration.GetConnectionString("DefaultDBConnection"));
            });
            services.AddTransient<ISessionService, SessionService>();

            services.AddScoped<IActionInvoker, BaseActionInvoker>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActionInvoker, BaseActionInvoker>();

            services.AddScoped<IRoleRepository,RoleRepository>();
            services.AddScoped<IRoleService,RoleService>();

            services.AddScoped<IIdentityService, AccountService>();
            return services;
          
        }
    }
}

