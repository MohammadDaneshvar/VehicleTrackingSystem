using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class CacheProviderService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheProviderService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheRequestAsync(string cacheKey, object request, TimeSpan time)
        {
            if (request == null) return;
            var serializedResponse = JsonConvert.SerializeObject(request);
            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = time
            });
        }
                
        public async Task<string> GetCachedRequestAsync(string cacheKey)
        {
            var cachedRequest = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrWhiteSpace(cachedRequest) ? null : cachedRequest;
        }
    }
}
