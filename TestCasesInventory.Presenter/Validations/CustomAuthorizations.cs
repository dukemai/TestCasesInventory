using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace TestCasesInventory.Presenter.Validations
{      
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if not logged, it will work as normal Authorize and redirect to the Login
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                //logged and wihout the role to access it - redirect to the custom controller action
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Errors", action = "AccessDenied" }));
            }
        }
    }
}
