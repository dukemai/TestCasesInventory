using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Business;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace TestCasesInventory.Controllers
{

    public class LoginStatusController : Controller
    {

        public ILoginStatusPresenter LoginStatusPresenter;

        public LoginStatusController()
        {
            LoginStatusPresenter = new LoginStatusPresenter();
        }
        // GET: LoginStatus
        //[ChildActionOnly]
        //[HttpGet]
        public ActionResult DisplayName()
        {

            if (User.Identity.IsAuthenticated)
            {
                var model = LoginStatusPresenter.GetCurrentUser(User.Identity.GetUserName());
                return PartialView("~/Views/Shared/_AuthenticatedPartial.cshtml", model);
            }
            else
            {
                return PartialView("~/Views/Shared/_UnAuthenticatedPartial.cshtml");
            }
        }
    }
}