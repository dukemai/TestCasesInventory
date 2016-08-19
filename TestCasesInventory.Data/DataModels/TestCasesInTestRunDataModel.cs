﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCasesInTestRunDataModel : DataModelBase
    {
        public string AssignedTo { get; set; } // UserID
        public string AssignedBy { get; set; } // UserID
        public int TestSuitID { get; set; }

        public int TestRunID { get; set; }
        public virtual TestRunDataModel TestRun { get; set; }
        public int TestCaseID { get; set; }
        public virtual TestCaseDataModel TestCase { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}