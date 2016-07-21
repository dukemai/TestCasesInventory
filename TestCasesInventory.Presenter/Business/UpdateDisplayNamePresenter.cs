using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class UpdateDisplayNamePresenter : PresenterBase, IUpdateDisplayNamePresenter
    {
        public UpdateDisplayNamePresenter() : base()
        {

        }
        public UpdateDisplayNameViewModel GetCurrentUserByEmail(string email)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindByEmail(email);

            var viewModel = new UpdateDisplayNameViewModel { DisplayName = currentUser.DisplayName };

            return viewModel;
        }

        public bool UpdateDisplayNameInDB(string UserId,  string NewDisplayName)
        {
            try
            {
                DataContext.Users.Find(UserId).DisplayName = NewDisplayName;
            }
            catch(Exception e)
            {
                return false;
            }

            DataContext.SaveChanges();
            return true;
        }


    }
}
