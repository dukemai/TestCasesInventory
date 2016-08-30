using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCasesInventory.Presenter.Business
{
    public class TestRunResultPresenter : ObservablePresenterBase<TestRunResultDataModel>, ITestRunResultPresenter
    {

        protected HttpContextBase HttpContext;
        protected ITestRunResultRepository testRunResultRepository;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected ITestRunRepository testRunRepository;
        protected ApplicationUserManager UserManager;
        protected RoleManager<IdentityRole> RoleManager;


        public TestRunResultPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            testRunResultRepository = new TestRunResultRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            testRunRepository = new TestRunRepository();
        }

        public TestRunResultViewModel GetTestRunResultById(int? testRunResultID)
        {
            if (!testRunResultID.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new Exception("Id was not valid.");
            }
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID.Value);
            if (testRunResult == null)
            {
                logger.Error("Test Run Result was not found.");
                throw new TestRunResultNotFoundException("Test Run Result was not found");
            }
            var testRunResultViewModel = testRunResult.MapTo<TestRunResultDataModel, TestRunResultViewModel>();
            return testRunResultViewModel;
        }

        public void InsertTestRunResult(CreateTestRunResultViewModel testRunResult)
        {
            var testRunResultDataModel = testRunResult.MapTo<CreateTestRunResultViewModel, TestRunResultDataModel>();
            testRunResultRepository.InsertTestRunResult(testRunResultDataModel);
            testRunResultRepository.Save();
            FeedObservers(testRunResultDataModel);
        }
        public void UpdateTestRunResult(int testRunResultID, EditTestRunResultViewModel testRunResult)
        {
            var testRunResultDataModel = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResultDataModel == null)
            {
                logger.Error("Test Run Result was not found.");
                throw new TestRunNotFoundException("Test Run Result was not found.");
            }
            else
            {
                testRunResultDataModel = testRunResult.MapTo(testRunResultDataModel);
                testRunResultRepository.UpdateTestRunResult(testRunResultDataModel);
                testRunResultRepository.Save();
            }
        }

        public void DeleteTestRunResult(int testRunResultID)
        {
            var testRunResultDataModel = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResultDataModel == null)
            {
                logger.Error("Test Run Result was not found.");
                throw new TestRunResultNotFoundException("Test Run Result was not found.");
            }
            else
            {
                //var testCaseResultsForTestRunResult = testCaseResultRepository.ListAll(testRunResultID);
                //foreach (var testCaseResult in testCaseResultsForTestRunResult)
                //{
                //    testCaseResultRepository.DeleteTestCaseResult(testCaseResult.ID);
                //    testCaseResultRepository.Save();
                //}
                testRunResultRepository.DeleteTestRunResult(testRunResultID);
                testRunResultRepository.Save();
            }
        }

        public IPagedList<TestRunResultViewModel> GetTestRunResults(FilterOptions options)
        {
            var list = testRunResultRepository.GetTestRunResults(options);
            var mappedList = list.MapTo<IPagedList<TestRunResultDataModel>, IPagedList<TestRunResultViewModel>>();
            return mappedList;
        }
        public CreateTestRunResultViewModel GetTestRunResultForCreate(int testRunId)
        {
            var testRun = testRunRepository.GetTestRunByID(testRunId);
            if (testRun == null)
            {
                throw new TestRunNotFoundException();
            }
            var model = new CreateTestRunResultViewModel();
            model.TestRunID = testRunId;
            return model;
        }

        public EditTestRunResultViewModel GetTestRunResultForEdit(int testRunResultID)
        {
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResult == null)
            {
                logger.Error("Test Run Result was not found.");
                throw new TestRunResultNotFoundException("Test Run Result was not found.");
            }
            var testRunResultViewModel = testRunResult.MapTo<TestRunResultDataModel, EditTestRunResultViewModel>();
            return testRunResultViewModel;
        }

        public List<TestCasesInTestRunViewModel> GetTestCasesAssignedToMe(int testRunId)
        {
            var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new UserNotFoundException("User was not found.");
            }

            var listTestCasesInTestRun = new List<TestCasesInTestRunViewModel>();
            var listTestCasesInTestRunDataModel = testCasesInTestRunRepository.GetTestCasesInTestRunAssignedToMe(user.Id, testRunId);
            foreach (var testCase in listTestCasesInTestRunDataModel)
            {
                listTestCasesInTestRun.Add(testCase.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>());
            }
            return listTestCasesInTestRun;
        }

        public List<TestCasesInTestRunViewModel> GetAllTestCases(int testRunId)
        {
            var listTestCasesInTestRun = new List<TestCasesInTestRunViewModel>();
            var listTestCasesInTestRunDataModel = testCasesInTestRunRepository.ListAll(testRunId);
            foreach (var testCase in listTestCasesInTestRunDataModel)
            {
                listTestCasesInTestRun.Add(testCase.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>());
            }
            return listTestCasesInTestRun;
        }

        public void FinishTestRun(int TestRunResultId)
        {
            var testRunResult = testRunResultRepository.GetTestRunResultByID(TestRunResultId);
            testRunResult.Status = ObjectStatus.Finished;
        }


    }
}
