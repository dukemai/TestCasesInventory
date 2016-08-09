using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter;

namespace TestCasesInventory
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(FilterOptions), new FilterOptionsBinding());
            StartUp.Start();
        }
    }
}
