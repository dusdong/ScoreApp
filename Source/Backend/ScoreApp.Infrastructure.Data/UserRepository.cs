using ScoreApp.Domain;
using ScoreApp.Domain.Factories;
using ScoreApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScoreApp.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IImageSearch imageSearch;
        private readonly dynamic userApp;

        public UserRepository(IUserAppFactory userAppFactory, IImageSearch imageSearch)
        {
            this.imageSearch = imageSearch;
            userApp = userAppFactory.Create();
        }

        public IEnumerable<User> GetAll()
        {
            var users = new Collection<User>();
            var result = userApp.User.Search(fields: new[] { "first_name", "last_name", "user_id" });
            foreach (var apiUser in result.Items)
                users.Add(Convert(apiUser));

            return users;
        }

        public IEnumerable<User> GetByIds(string[] ids)
        {
            var users = new Collection<User>();
            var result = userApp.User.Get(userId: ids, fields: new[] { "first_name", "last_name", "user_id" });
            foreach (var apiUser in result)
                users.Add(Convert(apiUser));

            return users;
        }

        private User Convert(dynamic apiUser)
        {
            return new User
            {
                Id = apiUser.UserId,
                FirstName = apiUser.FirstName,
                LastName = apiUser.LastName,
                Image = imageSearch.Search(apiUser.UserId)
            };
        }

        public User GetById(string id)
        {
            var result = userApp.User.Get(userId: id, fields: new[] { "first_name", "last_name", "user_id" });
            if (result.Length == 0)
                throw new ArgumentException(string.Format("Não foi possível encontrar o usuário com ID: {0}", id));

            return Convert(result[0]);
        }
    }
}
