using Newtonsoft.Json.Serialization;
using ScoreApp.Application;
using ScoreApp.Domain;
using ScoreApp.Domain.Factories;
using ScoreApp.Domain.Services;
using ScoreApp.Infrastructure.Caching;
using ScoreApp.Infrastructure.Data;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.WebApi;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace ScoreApp.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureIoC(config);
            ConfigureCamelCase(config);
            ConfigureModelBinders(config);
            config.MapHttpAttributeRoutes();
        }

        private static void ConfigureIoC(HttpConfiguration config)
        {
            //See https://simpleinjector.codeplex.com/wikipage?title=Web%20API%20Integration for more information on SimpleInjector WebApi integration.
            var container = new Container();
            container.Options.PropertySelectionBehavior = new SimpleInjectorPropertySelectionBehavior();
            container.RegisterWebApiControllers(config);
            container.RegisterWebApiFilterProvider(config);
            container.RegisterWebApiRequest<IScoreRepository, ScoreRepository>();
            container.RegisterWebApiRequest<IUserRepository, UserRepository>();
            container.RegisterWebApiRequest<IWitnessRepository, WitnessRepository>();
            container.RegisterWebApiRequest<IVoterRepository, VoterRepository>();
            container.RegisterWebApiRequest<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.RegisterDecorator(typeof(IUserRepository), typeof(CachedUserRepository));
            container.RegisterDecorator(typeof(IScoreRepository), typeof(CachedScoreRepository)); //CachedScoreRepository wraps ScoreRepository.
            container.RegisterDecorator(typeof(IScoreRepository), typeof(ExpirationScoreRepository)); //ExpirationScoreRepository wraps CachedScoreRepository.
            container.RegisterDecorator(typeof(IImageSearch), typeof(CachedImageSearch));
            container.Register<IImageSearch, ImageSearch>(Lifestyle.Singleton);
            container.Register<ISettings, SettingsAdapter>(Lifestyle.Singleton);
            container.Register<IUserAppFactory, UserAppFactory>(Lifestyle.Singleton);
            container.Register<IExpirationScoreFactory, ExpirationScoreFactory>(Lifestyle.Singleton);
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Singleton);
#if DEBUG
            container.Verify();
#endif
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void ConfigureCamelCase(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static void ConfigureModelBinders(HttpConfiguration config)
        {
            var provider = new SimpleModelBinderProvider(typeof(Pagination), new PaginationBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
        }
    }
}
