using ScoreApp.Domain.Services;

namespace ScoreApp.Domain.Factories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(bool transactional = false);
    }
}
