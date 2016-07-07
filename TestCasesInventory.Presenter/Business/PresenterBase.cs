using System.Data.Entity;
using TestCasesInventory.Data;

namespace TestCasesInventory.Presenter.Business
{
    /// <summary>
    /// base for all presenters
    /// </summary>
    public class PresenterBase
    {
        #region Properties

        protected DbContext DataContext;

        #endregion

        #region Methods

        public PresenterBase()
        {
            DataContext = new ApplicationDbContext();
        }

        #endregion
    }
}
