using NPoco;
using ScoreApp.Domain.Factories;

namespace ScoreApp.Infrastructure.Data
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private static IDatabase database;

        public IDatabase Get()
        {
            if (database == null)
                database = DatabaseInitializer.GetDatabase();

            return database;
        }
    }
}
