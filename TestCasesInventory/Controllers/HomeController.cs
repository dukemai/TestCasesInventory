using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCasesInventory.Controllers
{


    public class HomeController : Web.Common.Base.ControllerBase
    {
       

        public enum HomeMessageId
        {
            LoginSuccess,
            LogoutSucess,
            ResigterSuccess
        }




        public ActionResult Index(HomeMessageId? message)
        {
            //ViewBag.StatusMessage =
            //message == HomeMessageId.LoginSuccess ? "Congratulations! You has been loged in successful."
            //: message == HomeMessageId.LogoutSucess ? "Your accout has been loged off."
            //: message == HomeMessageId.ResigterSuccess ? "Congratulations! Register Successfully !You are logging in with email: " + User.Identity.Name
            //: "";
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if(IsAuthenticated)
            {
                return View("Authenticated/Index");
            }

            return View("Anonymous/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}