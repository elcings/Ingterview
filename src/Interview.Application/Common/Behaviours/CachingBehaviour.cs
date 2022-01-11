using Interview.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.Common.Behaviours
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable
    {
        private ILogger<CachingBehaviour<TRequest, TResponse>> _logger;
        private IMemoryCache _cache;
            
        public CachingBehaviour(ILogger<CachingBehaviour<TRequest, TResponse>> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName=request.GetType();

            _logger.LogInformation("{Request} is configured for caching", requestName);
            TResponse response;
            if (_cache.TryGetValue(request.CacheKey, out response))
            {
                return response;
            }

            response = await next();

            _cache.Set(request.CacheKey, response);
            return response;
        }
    }
}
