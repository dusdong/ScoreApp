using ScoreApp.Api;
using ScoreApp.Domain;
using System.Linq;

namespace System.Net.Http
{
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// In HttPost requests, you can't use the UserFilter attribute to get the logged user. In that case, use this method inside the action.
        /// </summary>
        public static User GetCurrentUser(this HttpRequestMessage request)
        {
            var cookie = request.Headers.GetCookies("ua_session_token").FirstOrDefault();
            if (cookie != null)
            {
                var userRepository = IoC.Current.GetInstance<IUserRepository>();
                return userRepository.GetByToken(cookie["ua_session_token"].Value);
            }

            return null;
        }
    }
}