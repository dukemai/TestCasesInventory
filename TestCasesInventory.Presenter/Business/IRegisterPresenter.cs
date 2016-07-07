using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface IRegisterPresenter : IPresenter<RegisterViewModel>
    {
        UserViewModel Register(RegisterViewModel model);

        Task<IdentityResult> CreateAsync(RegisterViewModel model);
    }
}
