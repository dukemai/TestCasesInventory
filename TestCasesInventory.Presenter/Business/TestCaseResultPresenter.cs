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

        #endregion


        #region Method


        public TestCaseResultPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            User = HttpContext.User;
            TestCaseResultRepository = new TestCaseResultRepository();
        }

        public void DeleteTestCaseResult(int testCaseResultID)
        {
            throw new NotImplementedException();
        }

        public TestCaseResultViewModel GetTestCaseResultById(int? testCaseResultID)
        {
            if (!testCaseResultID.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new Exception("Id was not valid.");
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

        public IPagedList<TestCaseResultViewModel> GetTestCasesForTestSuite(int testRunResultId, FilterOptions filterOptions)
        {
            var list = TestCaseResultRepository.GetTestCasesForTestSuite(testRunResultId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCaseResultDataModel>, IPagedList<TestCaseResultViewModel>>();
            return mappedList;
        }

        public void InsertTestCaseResult(CreateTestCaseResultViewModel testCaseResult)
        {
            if (!testCaseResult.TestRunResultID.HasValue)
            {
                logger.Error("Test Run was not found");
                throw new TestRunNotFoundException();
            }
            if (!testCaseResult.TestCasesInTestRunID.HasValue)
            {
                logger.Error("Test Case was not found");
                throw new TestCaseNotFoundException();
            }
            var testCaseResultDataModel = testCaseResult.MapTo<CreateTestCaseResultViewModel, TestCaseResultDataModel>();

            TestCaseResultRepository.InsertTestCaseResult(testCaseResultDataModel);
            TestCaseResultRepository.Save();
            //FeedObservers(testCaseResultDataModel);
        }

        public void UpdateTestCaseResult(int testCaseResultID, EditTestCaseResultViewModel testCaseResult)
        {
            var testCaseResultDataModel = TestCaseResultRepository.GetTestCaseResultByID(testCaseResultID);
            if (testCaseResultDataModel == null)
            {
                logger.Error("Test Case was not found.");
                throw new TestCaseNotFoundException("Test Case was not found.");
            }
            else
            {
                testCaseResultDataModel = testCaseResult.MapTo<EditTestCaseResultViewModel, TestCaseResultDataModel>(testCaseResultDataModel);
                TestCaseResultRepository.UpdateTestCaseResult(testCaseResultDataModel);
                TestCaseResultRepository.Save();
            }
        }

        public IDisposable Subscribe(IObserver<TestCaseResultDataModel> observer)
        {
            throw new NotImplementedException();
        }


        #endregion
    }

}