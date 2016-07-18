using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestCasesInventory.Data.Repository;
using TestCasesInventory.Presenter.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using TestCasesInventory.Data.DataModels;
using Microsoft.Owin.Security;
using System.Web.Mvc;

namespace TestCasesInventory.Presenter.Business
{
    public class UserPresenter : IUserPresenter
    {
        #region Properties

        protected HttpContextBase HttpContext;
        protected ApplicationUserManager UserManager;
        protected ApplicationSignInManager SignInManager;
        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        #region Methods

        public UserPresenter(HttpContextBase context)
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        }
            

        public UserViewModel Register(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<SignInStatus> PasswordSignInAsync(string email, string passWord, bool rememberMe, bool shouldLockOut)
        {
            return SignInManager.PasswordSignInAsync(email, passWord, rememberMe, shouldLockOut);
        }

        public Task SignInAsync(Data.DataModels.ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            return SignInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }

        public void SignOut()
        {
            AuthenticationManager.SignOut();
        }

        public Task<IdentityResult> CreateAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, DisplayName = model.DisplayName };

            return UserManager.CreateAsync(user, model.Password);
        }

        #endregion

    }

}
