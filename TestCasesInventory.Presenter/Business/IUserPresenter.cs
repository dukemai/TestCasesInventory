﻿using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface IUserPresenter : IPresenter<UserViewModel>, ILoginPresenter, IRegisterPresenter
    {
    }
}
