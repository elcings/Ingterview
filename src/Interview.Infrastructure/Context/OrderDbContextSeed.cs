﻿using Interview.Domain.AggregateModels.Buyer;
using Interview.Domain.AggregateModels.Orders;
using Interview.Domain.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Context
{
   public static class OrderDbContextSeed
    {
        public static async Task Seed (this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<OrderDbContext>();
                var logger = scope.ServiceProvider.GetService<ILogger>();

                bool useCustomizationData = false;
                var contentRootPath = "";

                context.Database.Migrate();

                if (!context.CardTypes.Any())
                {
                    context.CardTypes.AddRange(useCustomizationData
                                            ? GetCardTypesFromFile(contentRootPath, logger)
                                            : GetPredefinedCardTypes());

                    await context.SaveChangesAsync();
                }

                if (!context.OrderStatus.Any())
                {
                    context.OrderStatus.AddRange(useCustomizationData
                                        ? GetOrderStatusFromFile(contentRootPath, logger)
                                        : GetPredefinedOrderStatus());
                }

                await context.SaveChangesAsync();
            }
        
        
        }


        private static IEnumerable<CardType> GetCardTypesFromFile(string contentRootPath,ILogger logger)
        {
            string csvFileCardTypes = Path.Combine(contentRootPath, "Setup", "CardTypes.csv");

            if (!File.Exists(csvFileCardTypes))
            {
                return GetPredefinedCardTypes();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "CardType" };
                csvheaders = GetHeaders(requiredHeaders, csvFileCardTypes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedCardTypes();
            }

            int id = 1;
            return File.ReadAllLines(csvFileCardTypes)
                                        .Skip(1) // skip header column
                                        .SelectTry(x => CreateCardType(x, ref id))
                                        .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }

        private static CardType CreateCardType(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new Exception("Orderstatus is null or empty");
            }

            return new CardType(id++, value.Trim('"').Trim());
        }

        private static IEnumerable<CardType> GetPredefinedCardTypes()
        {
            return Enumeration.GetAll<CardType>();
        }

        private static IEnumerable<OrderStatus> GetOrderStatusFromFile(string contentRootPath, ILogger log)
        {
            string csvFileOrderStatus = Path.Combine(contentRootPath, "Setup", "OrderStatus.csv");

            if (!File.Exists(csvFileOrderStatus))
            {
                return GetPredefinedOrderStatus();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "OrderStatus" };
                csvheaders = GetHeaders(requiredHeaders, csvFileOrderStatus);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedOrderStatus();
            }

            int id = 1;
            return File.ReadAllLines(csvFileOrderStatus)
                                        .Skip(1) // skip header row
                                        .SelectTry(x => CreateOrderStatus(x, ref id))
                                        .OnCaughtException(ex => { log.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }

        private static OrderStatus CreateOrderStatus(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new Exception("Orderstatus is null or empty");
            }

            return new OrderStatus(id++, value.Trim('"').Trim().ToLowerInvariant());
        }

        private static IEnumerable<OrderStatus> GetPredefinedOrderStatus()
        {
            return new List<OrderStatus>()
        {
            OrderStatus.Submitted,
            OrderStatus.AwaitingValidation,
            OrderStatus.StockConfirmed,
            OrderStatus.Paid,
            OrderStatus.Shipped,
            OrderStatus.Cancelled
        };
        }

        private static string[] GetHeaders(string[] requiredHeaders, string csvfile)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() != requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }


        
    }
}
