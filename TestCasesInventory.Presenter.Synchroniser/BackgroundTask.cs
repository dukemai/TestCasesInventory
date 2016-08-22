using System;
using TestCasesInventory.Presenter.Synchroniser;

namespace TestCasesInventory.Presenter.Synchroniser
{
    public class BackgroundTask<T> : IBackgroundTask<T>
    {
        public Action Action { get; set; }
       
    }
}
