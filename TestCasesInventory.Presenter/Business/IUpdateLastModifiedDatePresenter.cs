﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Business
{
    public interface IUpdateLastModifiedDatePresenter
    {
        void UpdateLastModifiedDateInDB(string v, DateTime now);
    }
}