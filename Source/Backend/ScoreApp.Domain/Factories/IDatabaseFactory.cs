using NPoco;

namespace ScoreApp.Domain.Factories
{
    public interface IDatabaseFactory
    {
        IDatabase Get();
    }
}
