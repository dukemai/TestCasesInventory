using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface IRegisterPresenter:IPresenter<RegisterViewModel>
    {
        UserViewModel Register(RegisterViewModel model);
    }
}
