using ScoreApp.Infrastructure.Data;
using System.Web.Http;

namespace ScoreApp.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseInitializer.Setup();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
