using Interview.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Util
{
    public static class HostExtension
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder)
            where TContext:OrderDbContext
        {
            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();
                try
                {
                    var policy = Policy.Handle<SqlException>()
                    .WaitAndRetry(3, retry => TimeSpan.FromSeconds(3), (ex, time) =>
                    {
                        logger.LogError("An error occured while connecting to database used { Context} ", typeof(TContext).Name);
                    });

                    policy.Execute(() =>
                    {
                        context.Database.EnsureCreated();
                        context.Database.Migrate();
                        seeder(context, services);
                    });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,"An error occured while migrate to database used {Context}", typeof(TContext).Name);
                }
                return host;
                
            }
        }
    }
}
