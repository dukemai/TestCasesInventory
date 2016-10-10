using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Synchroniser;

namespace TestCasesInventory.Controllers
{
    [Authorize]
    public class AccountController : TestCasesInventory.Web.Common.Base.ControllerBase
    {
        #region Properties

        private IUserPresenter userPresenter;

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

        #region Methods

        public AccountController()
        {
            
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            UserPresenter.SignOut();
            return RedirectToAction("Index", "Home", new { Message = HomeController.HomeMessageId.LogoutSucess });
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateInput(false)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await UserPresenter.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home", new { Message = HomeController.HomeMessageId.LoginSuccess });
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.LastModifiedDate = DateTime.Now;
        //        var result = await UserPresenter.CreateAsync(model);
        //        if (result.Succeeded)
        //        {
        //            await UserPresenter.PasswordSignInAsync(model.Email, model.Password, false, false);

        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home", new { Message = HomeController.HomeMessageId.ResigterSuccess });
        //        }
        //        AddErrors(result);
        //    }

            // If we got this far, something failed, redisplay form
        //    return View(model);
        //}


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion
        
    }
}