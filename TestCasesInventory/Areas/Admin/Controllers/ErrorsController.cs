using System.Net;
using System.Web.Mvc;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult AccessDenied()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
    }
}