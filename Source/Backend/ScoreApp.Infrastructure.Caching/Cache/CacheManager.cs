using System;
using System.Runtime.Caching;

namespace ScoreApp.Infrastructure.Caching
{
    internal sealed class CacheManager
    {
        private readonly MemoryCache memoryCache;
        private static readonly CacheManager instance = new CacheManager();

        private CacheManager()
        {
            memoryCache = MemoryCache.Default;
        }

        public static CacheManager Instance
        {
            get { return instance; }
        }

        public object Get(string key)
        {
            return memoryCache.Get(key);
        }

        public void Update(string key, object value)
        {
            var item = memoryCache.GetCacheItem(key);
            item.Value = value;
        }

        public void Add(string key, object value, TimeSpan expiration)
        {
            memoryCache.Add(key, value, DateTime.Now.AddMilliseconds(expiration.TotalMilliseconds));
        }
    }
}
