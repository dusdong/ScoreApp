using ScoreApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoreApp.Infrastructure.Caching
{
    public class CachedUserRepository : IUserRepository
    {
        private const string allUsersKey = "All Users";
        private const string prefixUser = "User_";
        private readonly CacheManager cacheManager;
        private readonly IUserRepository repository;

        public CachedUserRepository(IUserRepository repository)
        {
            this.repository = repository;
            cacheManager = CacheManager.Instance;
        }

        public IEnumerable<User> GetAll()
        {
            var entry = cacheManager.Get(allUsersKey);
            if (entry != null)
                return (IEnumerable<User>)entry;

            var result = repository.GetAll();
            cacheManager.Add(allUsersKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public User GetById(string id)
        {
            var entry = cacheManager.Get(allUsersKey);
            if (entry != null) //there is a cached users collection.
            {
                var users = (ICollection<User>)entry;
                var user = users.FirstOrDefault(u => u.Id == id); //tries to find the user in the cached collection.
                if (user != null)
                    return user;

                //user not found in cached collection, so get it from repository, then cache it individually and also with collection.
                user = CacheIndividually(id);
                users.Add(user);
                cacheManager.Update(allUsersKey, users);
                return user;
            }
            else //there is no cached users collection, so tries to retrieve individual cached user.
            {
                var key = prefixUser + id;
                entry = cacheManager.Get(key);
                if (entry != null)
                    return (User)entry;

                //user not found individually cached, so get it from repository, then cache it individually.
                return CacheIndividually(id, key);
            }
        }

        private User CacheIndividually(string userId, string key = null)
        {
            key = key ?? prefixUser + userId;
            var user = repository.GetById(userId);
            cacheManager.Add(key, user, TimeSpan.FromDays(1));

            return user;
        }
    }
}
