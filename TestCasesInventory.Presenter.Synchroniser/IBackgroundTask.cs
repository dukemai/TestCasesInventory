using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Synchroniser
{
    public interface IBackgroundTask<T>
    {
        Action Action { get; set; }
    }
}
