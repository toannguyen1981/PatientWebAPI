using Microsoft.Extensions.Caching.Memory;
using System;

namespace PatientWebAPI.Helpers
{
    public class GlobalFunctions
    {
        public static T GetCacheItem<T>(IMemoryCache memoryCache, string key)
        {
            return memoryCache.Get<T>(key);
        }

        public static void AddCacheItem<T>(IMemoryCache memoryCache, string key, T value)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                .SetSize(1024);

            memoryCache.Set<T>(key, value, cacheOptions);
        }
    }
}
