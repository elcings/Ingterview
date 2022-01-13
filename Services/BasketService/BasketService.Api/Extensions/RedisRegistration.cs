using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Api.Extensions
{
    public static class RedisRegistration
    {
        public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider service, IConfiguration configuration)
        {
            var redisConf = ConfigurationOptions.Parse(configuration["ReddisSettings:ConnectionString"], true);
            redisConf.ResolveDns = true;
            return ConnectionMultiplexer.Connect(redisConf);
        }
    }
}
