using ScoreApp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public IEnumerable<User> GetByIds(string[] ids)
        {
            //Let's try not querying the repository, shall we?
            var idsNeeded = new List<string>(ids);
            var usersInCache = new Collection<User>();

            var entry = cacheManager.Get(allUsersKey);
            if (entry != null) //there is a cached users collection.
            {
                var users = (ICollection<User>)entry;
                foreach (var cached in users) //tries to find the users in the cached collection.
                {
                    if (idsNeeded.Contains(cached.Id))
                    {
                        idsNeeded.Remove(cached.Id); //found user in cache, so remove it from 'idsNeeded', so we don't need to query repository for this user.
                        usersInCache.Add(cached);
                    }
                }

                //if all users were in cache, return them.
                if (!idsNeeded.Any())
                    return usersInCache;

                //there are some users needed, so we try to retrieve them from individual cache.
                var removeIds = new Collection<string>();
                foreach (var idNeeded in idsNeeded)
                {
                    var key = prefixUser + idNeeded;
                    entry = cacheManager.Get(key);
                    if (entry != null)
                    {
                        removeIds.Add(idNeeded); //found user in individual cache, so mark it to be removed from 'idsNeeded'.
                        usersInCache.Add((User)entry);
                    }
                }
                idsNeeded.RemoveAll(i => removeIds.Contains(i));

                //if all users were in cache, return them.
                if (!idsNeeded.Any())
                    return usersInCache;

                //there are still some users needed, so get them from repository, then cache them individually.
                var repoUsers = repository.GetByIds(idsNeeded.ToArray());
                foreach (var repoUser in repoUsers)
                    CacheIndividually(repoUser);

                //we return the users in cache and the users found in repository.
                return usersInCache.Concat(repoUsers).ToList();
            }
            else //there is no cached users collection, so tries to retrieve individual cached user.
            {
                var removeIds = new Collection<string>();
                foreach (var idNeeded in idsNeeded)
                {
                    var key = prefixUser + idNeeded;
                    entry = cacheManager.Get(key);
                    if (entry != null)
                    {
                        removeIds.Add(idNeeded); //found user in individual cache, so mark it to be removed from 'idsNeeded'.
                        usersInCache.Add((User)entry);
                    }
                }
                idsNeeded.RemoveAll(i => removeIds.Contains(i));

                //if all users were in cache, return them.
                if (!idsNeeded.Any())
                    return usersInCache;

                //there are some users needed, so get them from repository, then cache them individually.
                var repoUsers = repository.GetByIds(idsNeeded.ToArray());
                foreach (var repoUser in repoUsers)
                    CacheIndividually(repoUser);

                //we return the users in cache and the users found in repository.
                return usersInCache.Concat(repoUsers).ToList();
            }
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
                user = repository.GetById(id);
                CacheIndividually(user);
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
                var user = repository.GetById(id);
                CacheIndividually(user, key);
                return user;
            }
        }

        private void CacheIndividually(User user, string key = null)
        {
            key = key ?? prefixUser + user.Id;
            cacheManager.Add(key, user, TimeSpan.FromDays(1));
        }
    }
}
