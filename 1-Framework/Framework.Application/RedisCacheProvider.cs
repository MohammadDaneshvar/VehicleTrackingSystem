using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }



        public async Task AddAsync<T>(string key, T value)
        {
            if (value != null)
            {

                var serializedResponse = JsonConvert.SerializeObject(value);
                await _distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
                {

                });
            }
        }

        public async Task<bool> ExistAsync(string key)
        {
            var result = false;
            var cachedRequest = await _distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cachedRequest))
                result = true;
            return result;
        }
        public async Task<T> GetAsync<T>(string key) where T : class
        {
            T result = null;
            var value = await _distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(value))
                result = JsonConvert.DeserializeObject<T>(value);
            return result;
        }


    }
}
