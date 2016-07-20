﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Presenter.Business
{
    public interface IUpdateDisplayNamePresenter
    {
        UpdateDisplayNameViewModel GetCurrentUserByEmail(string email);

        void UpdateDisplayNameInDB(string UserId, string OldDisplayName);
    }
}

