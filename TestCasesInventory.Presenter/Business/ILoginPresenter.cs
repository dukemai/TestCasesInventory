using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ILoginPresenter : IPresenter<LoginViewModel>
    {
        Task<SignInStatus> PasswordSignInAsync(string email, string passWord, bool rememberMe, bool shouldLockOut);

        Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser);
    }
}
