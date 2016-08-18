using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using TestCasesInventory.Data;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Config;
using TestCasesInventory.Presenter.Models;

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
        protected ITeamRepository TeamRepository;
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(UserPresenter));


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
            TeamRepository = new TeamRepository();
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
                logger.Error("User was not found");
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

            return SignInManager.PasswordSignInAsync(email.Trim(), passWord, rememberMe, shouldLockOut);
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
            var user = new ApplicationUser { UserName = model.Email.Trim(), Email = model.Email.Trim(), DisplayName = model.DisplayName.Trim(), LastModifiedDate = model.LastModifiedDate };

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
                logger.Error("User was not found");
                throw new UserNotFoundException();
            }
            string teamName = null;
            if(currentUser.TeamID != null)
            {
                var team = TeamRepository.GetTeamByID(currentUser.TeamID.Value);
                if(team == null)
                {
                    logger.Error("User was not found");
                    throw new TeamNotFoundException("Team was not found");
                }
                teamName = TeamRepository.GetTeamByID(currentUser.TeamID.Value).Name;
            }
            IndexViewModel model = new IndexViewModel { Email = currentUser.Email, DisplayName = currentUser.DisplayName,TeamName = teamName,TeamID = currentUser.TeamID, HasPassword = HasPassword(), UserRoles = String.Join(", ", UserManager.GetRoles(UserId)), LastModifiedDate = currentUser.LastModifiedDate };
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
                logger.Error("User was not found");
                throw new UserNotFoundException();
            }
            var viewModel = new IndexViewModel { Email = currentUser.Email.Trim(), LastModifiedDate = currentUser.LastModifiedDate };
            return viewModel;
        }

        public void UpdateDisplayNameInDB(string UserId, string NewDisplayName)
        {

            var currentUser = UserManager.FindById(UserId);
            if (currentUser == null)
            {
                logger.Error("User was not found");
                throw new UserNotFoundException();
            }
            currentUser.DisplayName = NewDisplayName;
            UserManager.Update(currentUser);
            HttpContext.GetOwinContext().Get<ApplicationDbContext>().SaveChanges();
        }

        public void UpdateLastModifiedDateInDB(string UserId, DateTime NewLastModifiedDate)
        {

            var currentUser = UserManager.FindById(UserId);
            if (currentUser == null)
            {
                logger.Error("User was not found");
                throw new UserNotFoundException();
            }
            currentUser.LastModifiedDate = NewLastModifiedDate;
            UserManager.Update(currentUser);
            HttpContext.GetOwinContext().Get<ApplicationDbContext>().SaveChanges();
        }

        public string GetUserProfilePictureUrl(string id)
        {
            var user = GetUserById(id);
            var folderPath = Path.Combine(UserConfigurations.PhotosFolderPath, user.Email, UserConfigurations.ProfileImageFileName);
            return folderPath;
        }
        public string GetUserProfilePictureUrlWithLastModifiedDate(string id)
        {
            var user = GetUserById(id);
            var u = UserManager.GetRoles(id);
            var UrlPath = Path.Combine(UserConfigurations.PhotosFolderPath, user.Email, UserConfigurations.ProfileImageFileName + "?" + user.LastModifiedDate);
            return UrlPath;
        }

        public string[] GetRolesForUser(string userID)
        {
            var rolesList = UserManager.GetRoles(userID) ;
            var roles = rolesList.ToArray();
            return roles;
        }
        #endregion

    }

}
