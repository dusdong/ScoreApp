using SimpleInjector;

namespace ScoreApp.Api
{
    /// <summary>
    /// This class should not be used very often. Try to inject dependencies through construtor or property, and if none
    /// of them is supported, then use this class.
    /// </summary>
    public sealed class IoC
    {
        private static readonly Container instance = new Container();

        private IoC() { }

        public static Container Current
        {
            get
            {
                return instance;
            }
        }
    }
}