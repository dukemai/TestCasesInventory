using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Business
{
    public interface IPresenter<T>
    {
        #region Methods

        T GetById(int id);

        List<T> ListAll();
        
        #endregion
    }
}
