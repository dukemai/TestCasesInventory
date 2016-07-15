using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Business;

namespace TestCasesInventory.Presenter.Business
{
    public interface ILogoutPresenter : IPresenter<object>
    {
        void SignOut();
    }
}
