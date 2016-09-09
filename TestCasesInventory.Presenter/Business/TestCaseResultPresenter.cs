using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Common;
using System.Linq;
using TestCasesInventory.Presenter.Business;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCaseResultPresenter : PresenterBase, ITestCaseResultPresenter
    {
        #region Properties

        protected HttpContextBase HttpContext;
        protected ApplicationUserManager UserManager;
        protected ApplicationSignInManager SignInManager;
        protected IPrincipal User;
        protected ITestCaseResultRepository TestCaseResultRepository;
        protected ITestRunResultRepository TestRunResultRepository;
        protected ITestCasesInTestRunRepository TestCaseInTestRunRepository;
        protected ITestRunRepository TestRunRepository;

        #endregion


        #region Method


        public TestCaseResultPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            User = HttpContext.User;
            TestCaseResultRepository = new TestCaseResultRepository();
            TestRunResultRepository = new TestRunResultRepository();
            TestCaseInTestRunRepository = new TestCasesInTestRunRepository();
            TestRunRepository = new TestRunRepository();
        }

        //public void DeleteTestCaseResult(int testCaseResultID)
        //{
        //    throw new NotImplementedException();
        //}

        public TestCaseResultViewModel GetTestCaseResultById(int? testCaseResultID)
        {
            if (!testCaseResultID.HasValue)
            {
                logger.Error("TestCaseResultID was not valid.");
                throw new Exception("TestCaseResultID was not valid.");
            }
            var testCaseResult = TestCaseResultRepository.GetTestCaseResultByID(testCaseResultID.Value);
            if (testCaseResult == null)
            {
                logger.Error("Test Case was not found.");
                throw new TestCaseNotFoundException("Test Case was not found.");
            }
            var testCaseResultViewModel = testCaseResult.MapTo<TestCaseResultDataModel, TestCaseResultViewModel>();
            return testCaseResultViewModel;
        }

        public TestCaseResultViewModel GetTestCaseResult(int testCasesInTestRunID, int testRunResultID)
        {
            var testCasesInTestRun = TestCaseInTestRunRepository.GetTestCaseInTestRunByID(testCasesInTestRunID);
            if (testCasesInTestRun == null)
            {
                logger.Error("TestCasesInTestRunID was not valid");
                throw new Exception("TestCasesInTestRunID was not valid");
            }
            var testRunResult = TestRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResult == null)
            {
                logger.Error("TestRunResultID was not valid");
                throw new Exception("TestRunResultID was not valid");
            }
            var testCaseResult = TestCaseResultRepository.GetTestCaseResult(testCasesInTestRunID, testRunResultID);

            if (testCaseResult == null)
            {
                logger.Error("Test Case was not found.");
                throw new TestCaseNotFoundException("Test Case was not found.");
            }

            var testCaseResultViewModel = testCaseResult.MapTo<TestCaseResultDataModel, TestCaseResultViewModel>();
            return testCaseResultViewModel;
        }


        public IPagedList<TestCaseResultViewModel> GetTestCasesForTestSuite(int testRunResultId, FilterOptions filterOptions)
        {
            var list = TestCaseResultRepository.GetTestCasesForTestSuite(testRunResultId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCaseResultDataModel>, IPagedList<TestCaseResultViewModel>>();
            return mappedList;
        }

        public void InsertOrUpdateTestCaseResult(CreateTestCaseResultViewModel testCaseResult)
        {
            testCaseResult.RunBy = User.Identity.GetUserId();
            testCaseResult.Created = testCaseResult.LastModified = User.Identity.GetUserName();
            testCaseResult.CreatedDate = testCaseResult.LastModifiedDate = DateTime.Now;
            var testCasesInTestRun = TestCaseInTestRunRepository.GetTestCaseInTestRunByID(testCaseResult.TestCasesInTestRunID);
            if (testCasesInTestRun == null)
            {
                logger.Error("TestCasesInTestRunID was not valid");
                throw new Exception("TestCasesInTestRunID was not valid");
            }
            var testRunResult = TestRunResultRepository.GetTestRunResultByID(testCaseResult.TestRunResultID);
            if (testRunResult == null)
            {
                logger.Error("TestRunResultID was not valid");
                throw new Exception("TestRunResultID was not valid");
            }

            var testCaseResultDataModel = TestCaseResultRepository.GetTestCaseResult(testCaseResult.TestCasesInTestRunID, testCaseResult.TestRunResultID);
            if (testCaseResultDataModel == null)
            {
                testCaseResultDataModel = testCaseResult.MapTo<CreateTestCaseResultViewModel, TestCaseResultDataModel>();
                TestCaseResultRepository.InsertTestCaseResult(testCaseResultDataModel);
                TestCaseResultRepository.Save();
            }
            else
            {
                var editTestCaseResult = testCaseResult.MapTo<CreateTestCaseResultViewModel, EditTestCaseResultViewModel>();
                testCaseResultDataModel = editTestCaseResult.MapTo<EditTestCaseResultViewModel, TestCaseResultDataModel>(testCaseResultDataModel);
                TestCaseResultRepository.UpdateTestCaseResult(testCaseResultDataModel);
                TestCaseResultRepository.Save();
            }

            //FeedObservers(testCaseResultDataModel);
        }

        //public void UpdateTestCaseResult(int testCaseResultID, EditTestCaseResultViewModel testCaseResult)
        //{
        //    var testCaseResultDataModel = TestCaseResultRepository.GetTestCaseResultByID(testCaseResultID);
        //    if (testCaseResultDataModel == null)
        //    {
        //        logger.Error("Test Case was not found.");
        //        throw new TestCaseNotFoundException("Test Case was not found.");
        //    }
        //    else
        //    {
        //        testCaseResultDataModel = testCaseResult.MapTo<EditTestCaseResultViewModel, TestCaseResultDataModel>(testCaseResultDataModel);
        //        TestCaseResultRepository.UpdateTestCaseResult(testCaseResultDataModel);
        //        TestCaseResultRepository.Save();
        //    }
        //}

        public IDisposable Subscribe(IObserver<TestCaseResultDataModel> observer)
        {
            throw new NotImplementedException();
        }





        #endregion
    }

}