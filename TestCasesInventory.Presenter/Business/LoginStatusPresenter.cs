using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class LoginStatusPresenter : ILoginStatusPresenter
    {
        public LoginStatusViewModel GetCurrentUser(string email)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindByEmail(email);
            var viewModel = new LoginStatusViewModel { DisplayName = currentUser.DisplayName };
            return viewModel;
        }


        //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //var currentUser = manager.FindById(User.Identity.GetUserId());


    }
}
