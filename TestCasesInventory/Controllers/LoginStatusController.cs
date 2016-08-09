using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Common;

namespace TestCasesInventory.Controllers
{

    public class LoginStatusController : Controller
    {
        public ILoginStatusPresenter LoginStatusPresenter;
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

        public LoginStatusController()
        {
            LoginStatusPresenter = new LoginStatusPresenter();
        }
        // GET: LoginStatus
        public ActionResult DisplayName()
        {
            try
            {
                var model = LoginStatusPresenter.GetCurrentUser(User.Identity.GetUserId());
                model.ProfilePictureURL = PathConfig.PhotosFolderPath + "/" + model.Email + "/" + PathConfig.ProfileName + "?_t=" + model.LastModifiedDate;
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    var profilePictureUrl = UserPresenter.GetUserProfilePictureUrl(userId);
                    model.ProfilePictureURL = profilePictureUrl.AppendVersioningQueryString(model.LastModifiedDate.Ticks.ToString());
                    model.IsProfilePictureExisted = Server.IsRelativePathExisted(profilePictureUrl);
                    return PartialView("~/Views/Shared/_AuthenticatedPartial.cshtml", model);
                }
                else
                {
                    return PartialView("~/Views/Shared/_UnAuthenticatedPartial.cshtml");
                }
            }
            catch (UserNotFoundException ex)
            {
                return PartialView("~/Views/Shared/_UnAuthenticatedPartial.cshtml");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}