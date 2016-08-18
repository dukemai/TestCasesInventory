using System.Data.Entity;
using TestCasesInventory.Data;

namespace TestCasesInventory.Presenter.Business
{
    /// <summary>
    /// base for all presenters
    /// </summary>
    public class PresenterBase
    {
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #region Methods

        public PresenterBase()
        {
        }

        #endregion
    }
}
