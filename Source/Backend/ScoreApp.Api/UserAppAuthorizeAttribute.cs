using ScoreApp.Domain;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ScoreApp.Api
{
    public class UserAppAuthorizeAttribute : AuthorizeAttribute
    {
        [SimpleInjectorProperty]
        public IUserRepository UserRepository { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var cookie = actionContext.Request.Headers.GetCookies("ua_session_token").FirstOrDefault();
            if (cookie == null)
                return false;

            var user = UserRepository.GetByToken(cookie["ua_session_token"].Value);
            return user != null;
        }
    }
}