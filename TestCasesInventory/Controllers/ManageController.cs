using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Data.DataModels;
using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Data;
using System.Collections;
using TestCasesInventory.Data.Common;
using System.Collections.Generic;
using System.IO;
using TestCasesInventory.Config;
using TestCasesInventory.Web.Common.Base;

namespace TestCasesInventory.Controllers
{
    [Authorize]
    public class ManageController : Web.Common.Base.ControllerBase
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

        public ManageController()
        {

        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error,
            NothingIsChosen,
            ChangeDisplayNameSuccess,
            ChangeRoleSuccess,
            ChangeProfilePictureSuccess
        }
        //
        // GET: /Manage/Index

        //
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.ChangeDisplayNameSuccess ? "Your name has been changed."
                : message == ManageMessageId.ChangeRoleSuccess ? "Your role has been changed."
                : message == ManageMessageId.ChangeProfilePictureSuccess ? "Your profile picture has been updated"
                : message == ManageMessageId.NothingIsChosen ? " You have not specified a file"
                : "";

            var userId = User.Identity.GetUserId();
            try
            {
                var model = UserPresenter.FindUserByID(userId);
                return View(model);
            }
            catch (UserNotFoundException e)
            {
                return View("UserNotFoundError");
            }

            //var model = UserPresenter
            //var model = new IndexViewModel
            //{
            //    HasPassword = UserPresenter.HasPassword(),
            //};

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
                }
                catch (UserNotFoundException e)
                {
                    return View("UserNotFoundError");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeDisplayNameSuccess });
            }
            return base.View();
        }




        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return base.View();
        }


        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.Identity.GetUserId();


                var result = await UserPresenter.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    var user = await UserPresenter.FindByIdAsync(userId);
                    if (user != null)
                    {
                        await UserPresenter.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }


                AddErrors(result);
            }
            catch (UserNotFoundException ex)
            {
                return View("UserNotFoundError");
            }

            catch (Exception ex)
            {
                return View("Error");

            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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
                ViewBag.Message = CustomMessages.NothingIsChosen;
                return View();
            }

            try
            {
                var userId = User.Identity.GetUserId();
                UploadUserProfileImage(file, userId);
                UserPresenter.UpdateLastModifiedDateInDB(userId, DateTime.Now);
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeProfilePictureSuccess });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error:" + ex.Message.ToString();
                return View();
            }
        }

        private void UploadUserProfileImage(HttpPostedFileBase file, string userId)
        {
            var profileImagePath = UserPresenter.GetUserProfilePictureUrl(userId);
            string serverPath = Server.MapPath(profileImagePath);
            file.SaveAs(serverPath);
        }
    }
}