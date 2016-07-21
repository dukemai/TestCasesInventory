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
        private IUserPresenter userPresenter;

        protected IUserPresenter UserPresenter
        {
            get
            {
                if (userPresenter == null)
                {
                    userPresenter = new UserPresenter(HttpContext);
                }
                return userPresenter;
            }
        }
        public ILoginStatusPresenter LoginStatusPresenter;

        public LoginStatusController()
        {
            LoginStatusPresenter = new LoginStatusPresenter();
        }
        // GET: LoginStatus
        public ActionResult DisplayName()
        {
            try
            {
                var model = LoginStatusPresenter.GetCurrentUser(User.Identity.GetUserName());
                if (User.Identity.IsAuthenticated)
                {
                    return PartialView("~/Views/Shared/_AuthenticatedPartial.cshtml", model);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch        
            {
                return PartialView("~/Views/Shared/_UnAuthenticatedPartial.cshtml");
            }

        }
    }
}