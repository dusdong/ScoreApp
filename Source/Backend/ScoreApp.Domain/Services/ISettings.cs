
namespace ScoreApp.Domain.Services
{
    public interface ISettings
    {
        string AuthenticationToken { get; }
        string AppId { get; }
    }
}
