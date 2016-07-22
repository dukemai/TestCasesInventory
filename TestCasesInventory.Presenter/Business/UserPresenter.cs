using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using TestCasesInventory.Data.DataModels;
using Microsoft.Owin.Security;
using System.Security.Principal;
using TestCasesInventory.Data.Common;

namespace TestCasesInventory.Presenter.Business
{
    public class UserPresenter : PresenterBase, IUserPresenter
    {
        #region Properties

        protected HttpContextBase HttpContext;
        protected ApplicationUserManager UserManager;
        protected ApplicationSignInManager SignInManager;
        protected IAuthenticationManager AuthenticationManager;
        protected IPrincipal User;
        protected RoleStore<IdentityRole> RoleStore;
        protected RoleManager<IdentityRole> RoleManager;

        #endregion

        #region Methods

        public UserPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            User = HttpContext.User;
            RoleStore = new RoleStore<IdentityRole>(DataContext);
            RoleManager = new RoleManager<IdentityRole>(RoleStore);
        }


        public UserViewModel Register(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }
        public Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = UserManager.FindById(userId);
                if (user == null)
            {
                throw new UserNotFoundException();
            }
            return UserManager.ChangePasswordAsync(userId, currentPassword, newPassword);

        }
        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return UserManager.FindByIdAsync(userId);
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


        public bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }


        //ManagePresenter
        public Task<string> GetPhoneNumberAsync(string userId)
        {
            return UserManager.GetPhoneNumberAsync(userId);
        }


        public Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            return UserManager.GetTwoFactorEnabledAsync(userId);
        }


        public Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            return UserManager.GetLoginsAsync(userId);
        }


        public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
        {
            return AuthenticationManager.TwoFactorBrowserRememberedAsync(userId);
        }

        //My Repo
        public IndexViewModel FindUserByID(string UserId)
        {
            var currentUser = UserManager.FindById(UserId);
            IndexViewModel model = new IndexViewModel { Email = currentUser.Email, DisplayName = currentUser.DisplayName, HasPassword = HasPassword(), UserRoles = String.Join(", ", UserManager.GetRoles(UserId)) };
            return model;
        }

        public bool IsRoleExist(string role)
        {
            var model = RoleManager.FindByName(role);
            if (model != null)
                return true;
            else
                return false;
        }

        public IdentityResult AddRole(string UserId, string UserRole)
        {
            return UserManager.AddToRole(UserId, UserRole);
        }

        public Task<IdentityResult> CreateRole(string UserRole)
        {
            return RoleManager.CreateAsync(new IdentityRole { Name = UserRole });
        }




        #endregion

    }

}
