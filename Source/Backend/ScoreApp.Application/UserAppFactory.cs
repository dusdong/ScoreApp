﻿using ScoreApp.Domain.Factories;
using ScoreApp.Domain.Services;
using UserApp;

namespace ScoreApp.Application
{
    public class UserAppFactory : IUserAppFactory
    {
        private readonly ISettings settings;

        public UserAppFactory(ISettings settings)
        {
            this.settings = settings;
        }

        public dynamic Create(string token = null)
        {
            return new API(settings.AppId, token ?? settings.AuthenticationToken);
        }
    }
}
