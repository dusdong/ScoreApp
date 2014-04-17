using ScoreApp.Domain.Services;
using System;

namespace ScoreApp.Infrastructure.Caching
{
    public class CachedImageSearch : IImageSearch
    {
        private const string prefixKey = "Profile_Image_";
        private readonly IImageSearch search;
        private readonly CacheManager cacheManager;

        public CachedImageSearch(IImageSearch search)
        {
            this.search = search;
            cacheManager = CacheManager.Instance;
        }

        public string Search(string userId)
        {
            var key = prefixKey + userId;
            var entry = cacheManager.Get(key);
            if (entry != null)
                return (string)entry;

            var result = search.Search(userId);
            cacheManager.Add(key, result, TimeSpan.FromDays(1));

            return result;
        }
    }
}
