using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class TestRunPresenter : ObservablePresenterBase<TestRunDataModel>, ITestRunPresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestRunRepository testRunRepository;
        protected ApplicationUserManager UserManager;
        protected ITeamRepository teamRepository;
        protected ITestCaseRepository testCaseRepository;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected RoleManager<IdentityRole> RoleManager;
        protected ITestSuiteRepository testSuiteRepository;


        public TestRunPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            testRunRepository = new TestRunRepository();
            teamRepository = new TeamRepository();
            testCaseRepository = new TestCaseRepository();
            testSuiteRepository = new TestSuiteRepository();
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        }

        public TestRunViewModel GetTestRunById(int? testRunID)
        {
            if (!testRunID.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new Exception("Id was not valid.");
            }
            var testRun = testRunRepository.GetTestRunByID(testRunID.Value);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            var testRunViewModel = testRun.MapTo<TestRunDataModel, TestRunViewModel>();
            return testRunViewModel;
        }

        public void InsertTestRun(CreateTestRunViewModel testRun)
        {
            if (!testRun.TeamID.HasValue)
            {
                logger.Error("User has not been assigned to any team.");
                throw new Exception("User has not been assigned to any team.");
            }
            var testRunDataModel = testRun.MapTo<CreateTestRunViewModel, TestRunDataModel>();

            testRunRepository.InsertTestRun(testRunDataModel);
            testRunRepository.Save();
            FeedObservers(testRunDataModel);
        }

        public void UpdateTestRun(int testRunID, EditTestRunViewModel testRun)
        {
            var testRunDataModel = testRunRepository.GetTestRunByID(testRunID);
            if (testRunDataModel == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            else
            {
                testRunDataModel = testRun.MapTo<EditTestRunViewModel, TestRunDataModel>(testRunDataModel);
                testRunRepository.UpdateTestRun(testRunDataModel);
                testRunRepository.Save();
            }
        }

        public void DeleteTestRun(int testRunID)
        {
            var testRunDataModel = testRunRepository.GetTestRunByID(testRunID);
            if (testRunDataModel == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            else
            {
                //var testCasesForTestRun = testCaseRepository.ListAll(testRunID);
                //foreach (var testCase in testCasesForTestRun)
                //{
                //    testCaseRepository.DeleteTestCase(testCase.ID);
                //    testCaseRepository.Save();
                //}
                testRunRepository.DeleteTestRun(testRunID);
                testRunRepository.Save();
            }
        }

        public List<TestSuiteInTestRunPopUpViewModel> GetTestSuitesPopUp(int testRunID)
        {
            var listTestSuitesPopUp = new List<TestSuiteInTestRunPopUpViewModel>();
            var listTestSuitesDataModel = testSuiteRepository.ListAll();
            foreach (var testSuite in listTestSuitesDataModel)
            {
                var testSuitePopUpViewModel = testSuite.MapTo<TestSuiteDataModel, TestSuiteInTestRunPopUpViewModel>();
                testSuitePopUpViewModel.TestRunID = testRunID;
                listTestSuitesPopUp.Add(testSuitePopUpViewModel);
            }
            return listTestSuitesPopUp;
        }

        public List<TestCaseInTestSuitePopUpViewModel> GetTestCasesInTestSuitePopUp(int testSuiteID, int testRunID)
        {
            var listTestCasesInTestSuitePopUp = new List<TestCaseInTestSuitePopUpViewModel>();
            var listTestCasesDataModel = testCaseRepository.ListAll(testSuiteID);
            foreach (var testCase in listTestCasesDataModel)
            {
                var testCaseInTestSuitePopUp = testCase.MapTo<TestCaseDataModel, TestCaseInTestSuitePopUpViewModel>();
                bool checkTestCaseInTestRun = testCasesInTestRunRepository.CheckTestCaseInTestRunByTestCaseID(testRunID, testCase.ID);
                if (checkTestCaseInTestRun)
                {
                    testCaseInTestSuitePopUp.IsInTestRun = true;
                    testCaseInTestSuitePopUp.TestRunID = testRunID;
                }
                listTestCasesInTestSuitePopUp.Add(testCaseInTestSuitePopUp);
            }
            return listTestCasesInTestSuitePopUp;
        }


        public IPagedList<TestRunViewModel> GetTestRuns(FilterOptions options, string userId)
        {
            var user = UserManager.FindById(userId);
            var getAll = UserManager.IsInRole(user.Id, PrivilegedUsersConfig.AdminRole);
            var list = testRunRepository.GetTestRuns(options, user.TeamID, getAll);
            var mappedList = list.MapTo<IPagedList<TestRunDataModel>, IPagedList<TestRunViewModel>>();
            return mappedList;
        }

        public CreateTestRunViewModel GetTestRunForCreate()
        {
            return new CreateTestRunViewModel();
        }

        public EditTestRunViewModel GetTestRunForEdit(int testRunID)
        {
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            var testRunViewModel = testRun.MapTo<TestRunDataModel, EditTestRunViewModel>();
            return testRunViewModel;
        }

        public CreateTestRunViewModel GetTestRunForAdminCreate(int? teamID)
        {
            var model = GetTestRunForCreate();
            model.TeamID = teamID;
            model.Teams = teamRepository.ListAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.ID.ToString()
            }).ToList();

            return model;
        }

        public EditTestRunViewModel GetTestRunForAdminEdit(int testRunID)
        {
            var model = GetTestRunForEdit(testRunID);

            model.Teams = teamRepository.ListAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.ID.ToString()
            }).ToList();

            return model;
        }
    }
}
