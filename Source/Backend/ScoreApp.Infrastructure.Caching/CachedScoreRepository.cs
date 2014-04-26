using ScoreApp.Domain;
using System;
using System.Collections.Generic;

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

        public void Save(SaveScore score)
        {
            repository.Save(score);
        }

        public void TimedUp(int scoreId)
        {
            repository.TimedUp(scoreId);
        }

        public Score GetById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<Score> GetAll(bool timeUp = false)
        {
            var key = keyBuilder.Create(GetType()).With(timeUp).Build();
            var entry = cacheManager.Get(key);
            if (entry != null)
                return (IEnumerable<Score>)entry;

            var result = repository.GetAll(timeUp);
            cacheManager.Add(key, result, TimeSpan.FromSeconds(2));

            return result;
        }
    }
}
