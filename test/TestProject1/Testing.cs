using Interview.Application;
using Interview.Application.Common.Models;
using Interview.Domain.Entities;
using Interview.Domain.Repositories;
using Interview.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration = null!;
        private static IServiceScopeFactory _scopeFactory = null!;
        private static Checkpoint _checkpoint = null!;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();


            var services = new ServiceCollection();

          

            services.AddLogging();

          
            services.AddApplication();

            services.AddInfrastructure(_configuration);




            _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };

            EnsureDatabase();
        }
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<CarDbContext>();

            context.Database.Migrate();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }


        
        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultDBConnection"));
        }

        public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<CarDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<CarDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task AddDistance(Distance distance)
        {
            using var scope = _scopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetRequiredService<IDistanceRepository>();

           await repository.Create(distance);

        }

        public static async Task AddError(Error err)
        {
            using var scope = _scopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetRequiredService<IErrorRepository>();

            await repository.Create(err);

        }

        public static async Task AddFuelLevel(FuelLevel level)
        {
            using var scope = _scopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetRequiredService<IFuelLevelRepository>();

            await repository.Create(level);

        }

        public static async Task<int> CountAsync<TEntity>() where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<CarDbContext>();

            return await context.Set<TEntity>().CountAsync();
        }
    }
}
