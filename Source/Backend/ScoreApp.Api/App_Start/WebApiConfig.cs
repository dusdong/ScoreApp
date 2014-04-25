using ScoreApp.Application;
using ScoreApp.Domain;
using ScoreApp.Domain.Services;
using ScoreApp.Infrastructure.Data;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SimpleInjector.Extensions;
using ScoreApp.Infrastructure.Caching;
using ScoreApp.Domain.Factories;

namespace ScoreApp.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //see https://simpleinjector.codeplex.com/wikipage?title=Web%20API%20Integration for more information.
            var container = new Container();
            //var webApiLifestyle = new WebApiRequestLifestyle();
            container.RegisterWebApiControllers(config);
            container.RegisterWebApiRequest<IScoreRepository, ScoreRepository>();
            container.RegisterWebApiRequest<IUserRepository, UserRepository>();
            container.RegisterDecorator(typeof(IUserRepository), typeof(CachedUserRepository));
            container.RegisterDecorator(typeof(IImageSearch), typeof(CachedImageSearch));
            container.Register<IExpirationDateCalculator, ExpirationDateCalculator>(Lifestyle.Singleton);
            container.Register<IImageSearch, ImageSearch>(Lifestyle.Singleton);
            container.Register<ISettings, SettingsAdapter>(Lifestyle.Singleton);
            container.Register<IUserAppFactory, UserAppFactory>(Lifestyle.Singleton);
#if DEBUG
            container.Verify();
#endif
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
