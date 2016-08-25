﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Validations;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(PrivilegedUsersConfig.AdminRole, PrivilegedUsersConfig.TesterRole)]
    public class TestCasesInTestRunController : Controller
    {
        #region Properties
        private ITestCasesInTestRunPresenter testCasesInTestRunPresenterObject;
        protected ITestCasesInTestRunPresenter TestCasePresenterObject
        {
            get
            {
                if (testCasesInTestRunPresenterObject == null)
                {
                    testCasesInTestRunPresenterObject = new TestCasesInTestRunPresenter(HttpContext);
                }
                return testCasesInTestRunPresenterObject;
            }
        }
        #endregion

        // GET: Admin/TestCasesInTestRun
        public ActionResult Index(/*int? testRunID, */[ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            
                int? testRunID = 2;
                if (testRunID == 2)
                {
                    var testCasesInTestRun = TestCasePresenterObject.GetTestCasesByTestRunID(testRunID.Value, filterOptions);
                    return View("Index", testCasesInTestRun);
                }
                else
                {
                    return View("Index");
                }
            
            
        }
    }
}