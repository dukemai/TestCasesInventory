using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using TestCasesInventory.Data.DataModels;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using System.Security.Principal;
using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Data;
using TestCasesInventory.Data.Common;


namespace TestCasesInventory.Presenter.Business
{
    public class UserPresenter : IUserPresenter
    {
        #region Properties

        protected HttpContextBase HttpContext;
        protected ApplicationUserManager UserManager;
        protected ApplicationSignInManager SignInManager;
        protected IAuthenticationManager AuthenticationManager;
        protected IPrincipal User;
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
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();

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
            if (currentUser == null)
            {
                throw new UserNotFoundException();
            }
            IndexViewModel model = new IndexViewModel { Email = currentUser.Email, DisplayName = currentUser.DisplayName, HasPassword = HasPassword(), UserRoles = String.Join(", ", UserManager.GetRoles(UserId)) };
            return model;
        }

        //public UpdateRolesViewModel FindUserById(string UserId)
        //{
        //    var currentUser = UserManager.FindById(UserId);

        //    if (currentUser == null)
        //    {
        //        throw new UserNotFoundException();
        //    }
        //    UpdateRolesViewModel model = new UpdateRolesViewModel { UserRoles = String.Join(", ", UserManager.GetRoles(UserId)) };
        //    return model;
        //}

        //public List<SelectListItem> AddRoleToList()
        //{
        //    var RoleNameList = RoleManager.Roles.Select(role=>role.Name).ToList();
        //    List<SelectListItem> RoleList = new List<SelectListItem>();
        //    for (int i = 0; i < RoleNameList.Count; i++)
        //    {
        //        RoleList.Add(new SelectListItem
        //         {
        //             Text = RoleNameList[i],
        //             Value = RoleNameList[i]
        //        });
        //    }
        //    return RoleList;
        //}

        //public bool IsRoleExist(string role)
        //{
        //    var model = RoleManager.FindByName(role);
        //    if (model != null)
        //        return true;
        //    else
        //        return false;
        //}

        //public IdentityResult AddRole(string UserId, string UserRole)
        //{
        //    return UserManager.AddToRole(UserId, UserRole);
        //}

        //public IdentityResult CreateRole(string UserRole)
        //{
        //    return RoleManager.Create(new IdentityRole { Name = UserRole });

        //}


        //public IdentityResult RemoveRole(string UserId, string UserRole)
        //{
        //    return UserManager.RemoveFromRole(UserId, UserRole);
        //}



        public UpdateDisplayNameViewModel GetCurrentUserById(string id)
        {
            var currentUser = UserManager.FindById(id);
            if (currentUser == null)
            {
                throw new UserNotFoundException();
            }
            var viewModel = new UpdateDisplayNameViewModel { DisplayName = currentUser.DisplayName.Trim() };
            return viewModel;
        }

        public IndexViewModel GetUserById(string id)
        {
            var currentUser = UserManager.FindById(id);
            if (currentUser == null)
            {
                throw new UserNotFoundException();
            }
            var viewModel = new IndexViewModel { Email = currentUser.Email.Trim() };
            return viewModel;
        }

        public void UpdateDisplayNameInDB(string UserId, string NewDisplayName)
        {

            var currentUser = UserManager.FindById(UserId);
            if (currentUser == null)
            {
                throw new UserNotFoundException();
            }
            currentUser.DisplayName = NewDisplayName;
            UserManager.Update(currentUser);
            HttpContext.GetOwinContext().Get<ApplicationDbContext>().SaveChanges();
        }




        #endregion

    }

}
