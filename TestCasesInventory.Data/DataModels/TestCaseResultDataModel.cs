﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCaseResultDataModel : DataModelBase
    {
        public int TestRunResultID { get; set; }
        public int TestCasesInTestRunID { get; set; }
        public string Status { get; set; }
        public string RunBy { get; set; }
        public string Comment { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        [ForeignKey("TestRunResultID")]
        public virtual TestRunResultDataModel TestRunResult { get; set; }
        [ForeignKey("TestCasesInTestRunID")]
        public virtual TestCasesInTestRunDataModel TestCase { get; set; }
        [ForeignKey("RunBy")]
        public virtual ApplicationUser User { get; set; }
    }
}