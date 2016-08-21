﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Models
{
    public class TestCasesInTestRunViewModel: ViewModelBase
    {
        public int ID { get; set; }
        public string TestCaseTitle { get; set; }
        public string TestRunTitle { get; set; }
        public int TestCaseID { get; set; }
        public int TestSuiteID { get; set; }
        public int TestRunID { get; set; }

        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }

    }
}