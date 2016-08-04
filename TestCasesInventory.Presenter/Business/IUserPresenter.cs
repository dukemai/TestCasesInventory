using System;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface IUserPresenter : IPresenter<UserViewModel>, ILoginPresenter, IRegisterPresenter, ILogoutPresenter, IManagePresenter, IChangePassPresenter, IUpdateDisplayNamePresenter, IUpdateLastModifiedDatePresenter
    {
        
    }
}
