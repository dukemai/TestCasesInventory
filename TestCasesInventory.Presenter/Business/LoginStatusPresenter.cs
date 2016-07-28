using System;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;

namespace TestCasesInventory.Presenter.Business
{
    public class LoginStatusPresenter : ILoginStatusPresenter
    {
        public LoginStatusViewModel GetCurrentUser(string email)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                //throw new DatabaseNotFoundException();
           
            var model = manager.FindByEmail(email);
            if (model == null)
            {
                throw new UserNotFoundException();
            }

            var viewModel = new LoginStatusViewModel();
            viewModel.DisplayName = model.DisplayName;
            return viewModel;

        }
    }
}
