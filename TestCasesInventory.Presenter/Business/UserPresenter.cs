using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestCasesInventory.Data.Repository;
using TestCasesInventory.Presenter.Models;
using Microsoft.AspNet.Identity.Owin;

namespace TestCasesInventory.Presenter.Business
{
    public class UserPresenter : IUserPresenter
    {
        public bool Delete(UserViewModel obj)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> ListAll()
        {
            throw new NotImplementedException();
        }

        public void Save(UserViewModel obj)
        {
            throw new NotImplementedException();
        }
    }

    public class RegisterUserPresenter : PresenterBase, IRegisterPresenter
    {
        #region Properties

        protected HttpContextBase HttpContext;
        protected ApplicationUserManager UserManager;

        #endregion

        public RegisterUserPresenter(HttpContextBase context)
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public bool Delete(RegisterViewModel obj)
        {
            throw new NotImplementedException();
        }

        public RegisterViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<RegisterViewModel> ListAll()
        {
            throw new NotImplementedException();
        }

        public UserViewModel Register(RegisterViewModel model)
        {
            if (UserManager == null)
            {
                throw new Exception("UserManager is not initialized properly");
            }

            
            return new UserViewModel();
        }
    }
}
