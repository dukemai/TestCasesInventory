using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Synchroniser
{
    public class BackgroundSynchroniser
    {
        #region Fields

        private static BackgroundSynchroniser synchroniser;
        private List<Task> tasks;

        #endregion

        #region Constructors

        public BackgroundSynchroniser()
        {
            tasks = new List<Task>();
        }

        #endregion

        #region Properties

        public static BackgroundSynchroniser Instance
        {
            get
            {
                if (synchroniser == null)
                {
                    synchroniser = new BackgroundSynchroniser();
                }

                return synchroniser;
            }
        }

        #endregion

        #region Methods

        public void AddNewTask<T>(BackgroundTask<T> task)
        {
            var action = new Task(() => task.Action());
            tasks.Add(action);
            action.Start();
            action.ContinueWith(t =>
            {
                tasks.Remove(action);
            });
        }

        #endregion
    }
}
