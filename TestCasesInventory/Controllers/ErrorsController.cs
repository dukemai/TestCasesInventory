using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TestCasesInventory.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult AccessDenied()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
    }
}