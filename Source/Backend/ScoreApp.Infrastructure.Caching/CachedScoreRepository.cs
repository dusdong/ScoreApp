using ScoreApp.Domain;
using System;

namespace ScoreApp.Infrastructure.Caching
{
    public class CachedScoreRepository : IScoreRepository
    {
        private readonly IScoreRepository repository;
        private readonly CacheKeyBuilder keyBuilder;
        private readonly CacheManager cacheManager;

        public CachedScoreRepository(IScoreRepository repository)
        {
            this.repository = repository;
            keyBuilder = new CacheKeyBuilder();
            cacheManager = CacheManager.Instance;
        }

        public Score Save(SaveScore score)
        {
            return repository.Save(score);
        }

        public Score GetById(int id)
        {
            return repository.GetById(id);
        }

        public IPagedResult<Score> GetAll(Pagination pagination, bool timeUp = false)
        {
            var key = keyBuilder.Create(GetType()).With(pagination, timeUp).Build();
            var entry = cacheManager.Get(key);
            if (entry != null)
                return (IPagedResult<Score>)entry;

            var result = repository.GetAll(pagination, timeUp);
            cacheManager.Add(key, result, TimeSpan.FromSeconds(2));

            return result;
        }
    }
}
