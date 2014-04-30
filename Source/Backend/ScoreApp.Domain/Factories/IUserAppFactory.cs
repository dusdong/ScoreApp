
namespace ScoreApp.Domain.Factories
{
    public interface IUserAppFactory
    {
        dynamic Create(string token = null);
    }
}
