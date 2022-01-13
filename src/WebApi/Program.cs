using Interview.Infrastructure;
using Interview.Infrastructure.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Util;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(GetConfiguration(), args);
            host.MigrateDbContext<OrderDbContext>((context, services) => {
              

                var seeder = new OrderDbContextSeed();
                var logger = services.GetRequiredService<ILogger<OrderDbContextSeed>>();
                seeder.Seed(context, logger).Wait();
            });

            host.Run();
        }


        static IConfiguration GetConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();
            return configBuilder.Build();
        }
        static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider((context, options) =>
            {

                options.ValidateOnBuild = false;
            })
            .ConfigureAppConfiguration(i => i.AddConfiguration(configuration))
            .UseStartup<Startup>().Build();




        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
