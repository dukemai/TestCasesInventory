using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Web.Common;

namespace TestCasesInventory.Controllers
{
    public class MenuController : Controller
    {
        protected PrivilegedUsersConfig privilegedUsers;

        public MenuController()
        {
            privilegedUsers = new PrivilegedUsersConfig();
        }


        // GET: Menu
        public ActionResult Index()
        {
            if (User.IsInRole(privilegedUsers.AdminRole))
            {
                return PartialView("~/Views/Shared/_AdminPartial.cshtml");
            }

            if (User.IsInRole(privilegedUsers.TesterRole))
            {
                return PartialView("~/Views/Shared/_TesterPartial.cshtml");
            }

            return PartialView("~/Views/Shared/_UnprivilegedPartial.cshtml");
        }
    }
}