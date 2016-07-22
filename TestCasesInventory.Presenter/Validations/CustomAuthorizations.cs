using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace TestCasesInventory.Presenter.Validations
{
    public class AdminAuthorizeAttribute: ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (!user.IsInRole("Admin") && user.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Errors", action = "AccessDenied" }));
            }
        }
    }
}
