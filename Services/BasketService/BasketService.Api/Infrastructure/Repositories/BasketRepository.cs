using BasketService.Api.Core.Application.Repositories;
using BasketService.Api.Core.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Api.Core.Infastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ILogger<BasketRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public BasketRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<BasketRepository>();
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
           return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var data= await _database.StringGetAsync(customerId);
            if (data.IsNullOrEmpty)
                return null;
            return JsonConvert.DeserializeObject<CustomerBasket>(data);

        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

        public IEnumerable<string> GetUsers(string customerId)
        {
            var server = GetServer();
            var data = server.Keys();

            return data?.Select(z => z.ToString());
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if (!created)
            {
                _logger.LogError("Problem occured duiring item created");
                return null;
            }
            _logger.LogInformation("BasketItems created");
            return await GetBasketAsync(basket.BuyerId);
        }
    }
}
