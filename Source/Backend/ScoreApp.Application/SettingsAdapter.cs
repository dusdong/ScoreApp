using ScoreApp.Domain.Services;
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
    }
}
