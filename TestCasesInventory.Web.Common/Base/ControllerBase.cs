using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Web.Common.Base
{
    public class ControllerBase : Controller
    {
        protected virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected virtual bool IsCurrentUserAdmin()
        {
            return User.IsInRole(PrivilegedUsersConfig.AdminRole);
        }

        protected virtual bool IsCurrentUserTester()
        {
            return User.IsInRole(PrivilegedUsersConfig.TesterRole);
        }
    }
}
