using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ILogoutPresenter : IPresenter<UserViewModel>
    {
        void SignOut();
    }
}
