using ScoreApp.Domain.Services;
using System;
using System.Configuration;

namespace ScoreApp.Application
{
    public class SettingsAdapter : ISettings
    {
        public string AuthenticationToken
        {
            get { return ConfigurationManager.AppSettings["AuthenticationToken"]; }
        }

        public string AppId
        {
            get { return ConfigurationManager.AppSettings["AppId"]; }
        }

        public TimeSpan ScoreExpirationTime
        {
            get { return TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["ScoreExpirationTimeInMinutes"])); }
        }
    }
}
