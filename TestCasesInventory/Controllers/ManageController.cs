using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Data.Common;
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

        //public ActionResult EditUserRole()
        //{
        //try
        //{
        //    var model = new UpdateRolesViewModel();
        //    model = UserPresenter.FindUserRoleById(User.Identity.GetUserId());
        //    return View(model);
        //}
        //catch (UserNotFoundException e)
        //{
        //    return View("UserNotFoundError");
        //}
        //catch (Exception ex)
        //{
        //    return View("Error");
        //}

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult EditUserRole(UpdateRolesViewModel model, string command)
        //{

        //    if (command.Equals("Save Change"))
        //    {
        //        return RedirectToAction("Index", new { Message = ManageMessageId.ChangeRoleSuccess });
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (command.Equals("Add"))
        //        {
        //            if (!UserPresenter.IsRoleExist(model.UserRoles))
        //            {
        //                UserPresenter.CreateRole(model.UserRoles);
        //            }
        //            UserPresenter.AddRole(User.Identity.GetUserId(), model.UserRoles);

        //        }
        //        if (command.Equals("Remove"))
        //        {
        //            if (UserPresenter.IsRoleExist(model.UserRoles))
        //            {
        //                UserPresenter.RemoveRole(User.Identity.GetUserId(), model.UserRoles);
        //            }
        //        }


        //return View();
        //    }

        // return View();
        //}

        //public PartialViewResult ShowCurrentRole()
        //{
        //    var viewModel = new UpdateRolesViewModel();
        //    viewModel = UserPresenter.FindUserById(User.Identity.GetUserId());
        //    return PartialView("CurrentRolePartialView", viewModel);
        // }

        [HttpGet]
        public ActionResult ChangeProfilePicture()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ChangeProfilePicture(HttpPostedFileBase file)
        {


            if (file != null && file.ContentLength > 0)
                try
                {
                    UserPresenter.UpdateLastModifiedDateInDB(User.Identity.GetUserId(), DateTime.Now);
                    var model = UserPresenter.GetUserById(User.Identity.GetUserId());
                    string folderPath = Path.Combine(Server.MapPath(PathConfig.PhotosFolderPath), model.Email);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string path = Path.Combine(Server.MapPath(PathConfig.PhotosFolderPath), model.Email, PathConfig.ProfileName);
                    file.SaveAs(path);

                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeProfilePictureSuccess });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error:" + ex.Message.ToString();
                    return View();
                }
            else
            {
                ViewBag.Message = CustomMessages.NothingIsChosen;
                return View();
            }

            //}
            /*

            //
            // POST: /Manage/RemoveLogin
            [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
                {
                    ManageMessageId? message;
                    var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                    else
                    {
                        message = ManageMessageId.Error;
                    }
                    return RedirectToAction("ManageLogins", new { Message = message });
                }

                //
                // GET: /Manage/AddPhoneNumber
                public ActionResult AddPhoneNumber()
                {
                    return View();
                }

                //
                // POST: /Manage/AddPhoneNumber
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    // Generate the token and send it
                    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
                    if (UserManager.SmsService != null)
                    {
                        var message = new IdentityMessage
                        {
                            Destination = model.Number,
                            Body = "Your security code is: " + code
                        };
                        await UserManager.SmsService.SendAsync(message);
                    }
                    return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
                }

                //
                // POST: /Manage/EnableTwoFactorAuthentication
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> EnableTwoFactorAuthentication()
                {
                    await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", "Manage");
                }

                //
                // POST: /Manage/DisableTwoFactorAuthentication
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> DisableTwoFactorAuthentication()
                {
                    await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", "Manage");
                }

                //
                // GET: /Manage/VerifyPhoneNumber
                public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
                {
                    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
                    // Send an SMS through the SMS provider to verify the phone number
                    return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
                }

                //
                // POST: /Manage/VerifyPhoneNumber
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                        return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
                    }
                    // If we got this far, something failed, redisplay form
                    ModelState.AddModelError("", "Failed to verify phone");
                    return View(model);
                }

                //
                // GET: /Manage/RemovePhoneNumber
                public async Task<ActionResult> RemovePhoneNumber()
                {
                    var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
                    if (!result.Succeeded)
                    {
                        return RedirectToAction("Index", new { Message = ManageMessageId.Error });
                    }
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
                }

                //
                 GET: /Manage/ChangePassword
                public ActionResult ChangePassword()
                {
                    return View();
                }


                 POST: /Manage/ChangePassword
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                        return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    AddErrors(result);
                    return View(model);
                }

                //
                // GET: /Manage/SetPassword
                public ActionResult SetPassword()
                {
                    return View();
                }

                //
                // POST: /Manage/SetPassword
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                        var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                        if (result.Succeeded)
                        {
                            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                            if (user != null)
                            {
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            }
                            return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                        }
                        AddErrors(result);
                    }

                    // If we got this far, something failed, redisplay form
                    return View(model);
                }

                //
                // GET: /Manage/ManageLogins
                public async Task<ActionResult> ManageLogins(ManageMessageId? message)
                {
                    ViewBag.StatusMessage =
                        message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                        : message == ManageMessageId.Error ? "An error has occurred."
                        : "";
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user == null)
                    {
                        return View("Error");
                    }
                    var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
                    var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
                    ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
                    return View(new ManageLoginsViewModel
                    {
                        CurrentLogins = userLogins,
                        OtherLogins = otherLogins
                    });
                }

                //
                // POST: /Manage/LinkLogin
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult LinkLogin(string provider)
                {
                    // Request a redirect to the external login provider to link a login for the current user
                    return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
                }

                //
                // GET: /Manage/LinkLoginCallback
                public async Task<ActionResult> LinkLoginCallback()
                {
                    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
                    if (loginInfo == null)
                    {
                        return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
                    }
                    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
                    return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
                }

                protected override void Dispose(bool disposing)
                {
                    if (disposing && _userManager != null)
                    {
                        _userManager.Dispose();
                        _userManager = null;
                    }

                    base.Dispose(disposing);
                }

        #region Helpers
                // Used for XSRF protection when adding external logins
                private const string XsrfKey = "XsrfId";

                private IAuthenticationManager AuthenticationManager
                {
                    get
                    {
                        return HttpContext.GetOwinContext().Authentication;
                    }
                }

                private void AddErrors(IdentityResult result)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                private bool HasPassword()
                {
                    var user = UserManager.FindById(User.Identity.GetUserId());
                    if (user != null)
                    {
                        return user.PasswordHash != null;
                    }
                    return false;
                }

                private bool HasPhoneNumber()
                {
                    var user = UserManager.FindById(User.Identity.GetUserId());
                    if (user != null)
                    {
                        return user.PhoneNumber != null;
                    }
                    return false;
                }



        #endregion
            }
             * */
        }
    }

}