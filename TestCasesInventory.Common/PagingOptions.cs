﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Common
{
    public class PagingOptions
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}
