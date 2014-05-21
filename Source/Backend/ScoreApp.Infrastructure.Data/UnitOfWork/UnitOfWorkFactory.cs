using ScoreApp.Domain.Factories;
using ScoreApp.Domain.Services;

namespace ScoreApp.Infrastructure.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDatabaseFactory factory;

        public UnitOfWorkFactory(IDatabaseFactory factory)
        {
            this.factory = factory;
        }

        public IUnitOfWork Create(bool transactional = false)
        {
            var database = factory.Get();
            return new NPocoUnitOfWork(database, transactional);
        }
    }
}
