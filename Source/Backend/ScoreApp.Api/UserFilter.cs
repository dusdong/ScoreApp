using ScoreApp.Domain;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ScoreApp.Api
{
    public class UserFilter : ActionFilterAttribute
    {
        [SimpleInjectorProperty]
        public IUserRepository UserRepository { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (!actionContext.ActionDescriptor.GetParameters().Any(p => p.ParameterType == typeof(User)))
                return;

            var cookie = actionContext.Request.Headers.GetCookies("ua_session_token").FirstOrDefault();
            if (cookie != null)
            {
                var parameter = actionContext.ActionDescriptor.GetParameters().First(p => p.ParameterType == typeof(User));
                actionContext.ActionArguments[parameter.ParameterName] = UserRepository.GetByToken(cookie["ua_session_token"].Value);
            }
        }
    }
}