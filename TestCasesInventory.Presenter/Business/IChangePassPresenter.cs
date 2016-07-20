using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;


namespace TestCasesInventory.Presenter.Business
{
    public interface IChangePassPresenter : IPresenter<ChangePasswordViewModel>
    {
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<ApplicationUser> FindByIdAsync(string userId);
    }
}
