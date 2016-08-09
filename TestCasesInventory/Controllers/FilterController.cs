using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCasesInventory.Controllers
{
    public class FilterController : TestCasesInventory.Web.Common.Base.ControllerBase
    {
        public ActionResult FilterForTestSuite()
        {
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml");
        }
    }
}