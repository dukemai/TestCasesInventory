using System.Linq;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Common;

namespace TestCasesInventory.Presenter.Business
{
    public class LoginStatusPresenter : ILoginStatusPresenter
    {
        public LoginStatusViewModel GetCurrentUser(string id)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //throw new DatabaseNotFoundException();

            var model = manager.FindById(id);
            if (model == null)
            {
                throw new UserNotFoundException();
            }

            var viewModel = new LoginStatusViewModel();
            viewModel.DisplayName = model.DisplayName;
            viewModel.Email = model.Email;
            viewModel.UserName = model.UserName;
            viewModel.LastModifiedDate = model.LastModifiedDate;
            viewModel.Role = manager.IsInRole(id, PrivilegedUsersConfig.AdminRole) ? PrivilegedUsersConfig.AdminRole : PrivilegedUsersConfig.TesterRole;
            return viewModel;

        }
    }
}
