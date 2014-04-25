
namespace ScoreApp.Infrastructure.Caching
{
    internal interface ICacheKeyParameters
    {
        ICacheKeyBuilder With(params object[] parameters);
        ICacheKeyBuilder WithoutParameters();
    }
}
