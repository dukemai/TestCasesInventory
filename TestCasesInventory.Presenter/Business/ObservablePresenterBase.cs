using System;
using System.Collections.Generic;
using TestCasesInventory.Common;

namespace TestCasesInventory.Presenter.Business
{
    public class ObservablePresenterBase<T> : PresenterBase, IObservable<T>
    {
        #region Fields

        protected List<IObserver<T>> observers;

        #endregion

        public ObservablePresenterBase() : base()
        {
            observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<T>(observers, observer);
        }

        protected void FeedObservers(T user)
        {
            foreach (var observer in observers)
                observer.OnNext(user);
        }
    }
}
