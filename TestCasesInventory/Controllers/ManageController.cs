using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Config;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Synchroniser;
using TestCasesInventory.Web.Common.Base;
using TestCasesInventory.Web.Common.Utils;

namespace TestCasesInventory.Controllers
{


    [Authorize]
    public class ManageController : Web.Common.Base.ControllerBase
    {

        #region Fields

        private IUserPresenter userPresenter;

        #endregion

        #region Properties


        protected IUserPresenter UserPresenter
        {
            get
            {
                if (userPresenter == null)
                {
                    userPresenter = new UserPresenter(HttpContext);
                    userPresenter.Subscribe(new UsersObserver());
                }
                return userPresenter;
            }
        }

        #endregion

        #region Constructors

        public ManageController()
        {

        }

        #endregion

        #region Actions        

        //
        // GET: /Manage/Index

        //
        public async Task<ActionResult> Index(string Message)
        {

            ViewBag.StatusMessage = Message;
            var userId = User.Identity.GetUserId();
            try
            {
                var model = UserPresenter.FindUserByID(userId);
                var profilePictureUrl = UserPresenter.GetUserProfilePictureUrl(userId);
                model.ProfilePictureURL = profilePictureUrl.AppendVersioningQueryString(model.LastModifiedDate.Ticks.ToString());
                model.IsProfilePictureExisted = Server.IsRelativePathExisted(profilePictureUrl);
                return View(model);
            }
            catch (UserNotFoundException e)
            {
                return View("UserNotFoundError");
            }
        }


        [HttpGet]
        public ActionResult EditDisplayName()
        {
            try
            {
                var model = UserPresenter.GetCurrentUserById(User.Identity.GetUserId());
                return View(model);
            }
            catch (UserNotFoundException e)
            {
                return View("UserNotFoundError");
            }
            catch (Exception e)
            {
                throw e;
            }



        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditDisplayName(UpdateDisplayNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserPresenter.UpdateDisplayNameInDB(User.Identity.GetUserId(), model.DisplayName);
                    UserPresenter.UpdateLastModifiedDateInDB(User.Identity.GetUserId(), DateTime.Now);
                }
                catch (UserNotFoundException e)
                {
                    return View("UserNotFoundError");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction("Index", new { Message = ActionConfirmMessages.ChangeDisplayNameSuccess });
            }
            return base.View();
        }

        [HttpGet]
        public ActionResult ChangeProfilePicture()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ChangeProfilePicture(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                ViewBag.Message = ActionConfirmMessages.NothingIsChosen;
                return View();
            }

            try
            {
                var userId = User.Identity.GetUserId();
                UserPresenter.UpdateLastModifiedDateInDB(userId, DateTime.Now);
                UploadUserProfileImage(file, userId);
                return RedirectToAction("Index", new { Message = ActionConfirmMessages.ChangeProfilePictureSuccess });
            }
            catch (ImageTypeException ex)
            {
                ViewBag.Message = ActionConfirmMessages.ImageTypeError;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error:" + ex.Message.ToString();
                return View();
            }
        }

        #endregion


        private void UploadUserProfileImage(HttpPostedFileBase file, string userId)
        {
            var profileImagePath = UserPresenter.GetUserProfilePictureUrl(userId);
            var serverPath = Server.MapPath(profileImagePath);
            PathHelper.EnsureDirectories(serverPath);
            if (!ImageExtensionConfig.ImageExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                throw new ImageTypeException();
            }
            file.SaveAs(serverPath);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}