﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITeamPresenter : IPresenter<TeamViewModel>
    {
        List<TeamViewModel> ListAll();
    }
}
