using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface IManagePresenter
    {
        bool IsAccountExist(string email);
        bool HasPassword();
        Task<string> GetPhoneNumberAsync(string userId);
        Task<bool> GetTwoFactorEnabledAsync(string userId);
        Task<IList<UserLoginInfo>> GetLoginsAsync(string userId);
        Task<bool> TwoFactorBrowserRememberedAsync(string userId);
        //Return a model
        IndexViewModel FindUserByID(string UserId);
        Task CheckAndRegister(bool IsUserValid, LoginViewModel model);
        //UpdateRolesViewModel FindUserById(string UserId);
        //List<SelectListItem> AddRoleToList();
        //bool IsRoleExist(string role);
        //IdentityResult AddRole(string UserId, string UserRole);
        //IdentityResult CreateRole(string UserRole);
        //IdentityResult RemoveRole(string UserId, string UserRole);
    }
}
