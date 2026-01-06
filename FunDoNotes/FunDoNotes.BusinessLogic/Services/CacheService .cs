using FunDoNotes.BusinessLogic.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key)
        {
            var data = _cache.GetString(key);
            if (data == null) return default;
            return JsonSerializer.Deserialize<T>(data);
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            var json = JsonSerializer.Serialize(value);
            _cache.SetString(key, json, options);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
