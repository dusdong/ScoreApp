using NPoco;
using NPoco.FluentMappings;
using ScoreApp.Infrastructure.Data.Mappings;
using System;

namespace ScoreApp.Infrastructure.Data
{
    public static class DatabaseFactory
    {
        private static NPoco.DatabaseFactory factory;

        public static IDatabase GetDatabase()
        {
            if (factory == null)
                throw new InvalidOperationException("Setup method was not called");

            return factory.GetDatabase();
        }

        public static void Setup()
        {
            var mappings = FluentMappingConfiguration.Configure(new SaveScoreMapping());

            factory = NPoco.DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new Database("ApplicationConnection"));
                x.WithFluentConfig(mappings);
            });
        }
    }
}
