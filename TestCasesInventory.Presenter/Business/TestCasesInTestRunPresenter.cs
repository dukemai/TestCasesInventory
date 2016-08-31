﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCasesInTestRunPresenter : PresenterBase, ITestCasesInTestRunPresenter
    {
        #region Fields

        protected HttpContextBase HttpContext;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected ITestSuiteRepository testSuiteRepository;
        protected ITestCaseRepository testCaseRepository;
        protected ITestRunRepository testRunRepository;
        protected ITeamRepository teamRepository;
        protected ApplicationUserManager UserManager;

        #endregion

        #region Constructors

        public TestCasesInTestRunPresenter(HttpContextBase context)
        {
            HttpContext = context;
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            testSuiteRepository = new TestSuiteRepository();
            testCaseRepository = new TestCaseRepository();
            testRunRepository = new TestRunRepository();
            teamRepository = new TeamRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        #endregion

        #region Methods        

        public List<TestCasesInTestRunViewModel> GetTestCasesInTestRun(int testRunID)
        {
            var testCasesInTestRunDatas = testCasesInTestRunRepository.GetTestCasesInTestRun(testRunID);
            var testCasesInTestRunViews = new List<TestCasesInTestRunViewModel>();
            foreach (var testCaseInTestRun in testCasesInTestRunDatas)
            {
                CheckExceptionTestCaseInTestRun(testCaseInTestRun);
                var testCaseInTestRunView = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>();
                testCasesInTestRunViews.Add(testCaseInTestRunView);
            }
            return testCasesInTestRunViews;
        }

        public void AddTestCasesToTestRun(List<int> testCasesIDs, int testRunID)
        {
            var user = UserManager.FindById(HttpContext.User.Identity.GetUserId());
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new UserNotFoundException("Current User Is Not Identified.");
            }
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            var testCasesInTestRunData = new List<TestCasesInTestRunDataModel>();
            foreach (var testCaseID in testCasesIDs)
            {
                var testCaseData = testCaseRepository.GetTestCaseByID(testCaseID);
                if (testCaseData == null)
                {
                    logger.Error("Test Case with id = " + testCaseID + " was not found.");
                    throw new TestCaseNotFoundException("Test Case with id = " + testCaseID + " was not found.");
                }
                var testCaseInTestRunViewModel = new CreateTestCasesInTestRunViewModel
                {
                    TestCaseID = testCaseID,
                    TestRunID = testRunID,
                    TestSuiteID = testCaseData.TestSuiteID,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    Created = user.Email,
                    LastModified = user.Email,
                    AssignedTo = user.Id,
                    AssignedBy = user.Id,
                };
                var testCaseInTestRunData = testCaseInTestRunViewModel.MapTo<CreateTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>();
                testCasesInTestRunData.Add(testCaseInTestRunData);
            }
            testCasesInTestRunRepository.AddTestCasesToTestRun(testCasesInTestRunData);
            testCasesInTestRunRepository.Save();
        }


        public void RemoveTestCasesFromTestRun(List<int> testCasesIDs, int testRunID)
        {
            var user = UserManager.FindById(HttpContext.User.Identity.GetUserId());
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new UserNotFoundException("Current User Is Not Identified.");
            }
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            testCasesInTestRunRepository.RemoveTestCasesFromTestRun(testCasesIDs, testRunID);
            testCasesInTestRunRepository.Save();
        }

        public IList<UserPopUpViewModel> GetUsersPopUp(int testCaseInTestRunID)
        {
            var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(testCaseInTestRunID);
            CheckExceptionTestCaseInTestRun(testCaseInTestRun);
            var testRun = testRunRepository.GetTestRunByID(testCaseInTestRun.TestRunID);
            var usersBelongTeam = teamRepository.GetUsersByTeamID(testRun.TeamID);
            var usersPopUpViewModel = new List<UserPopUpViewModel>();
            foreach (var user in usersBelongTeam)
            {
                var userViewModel = user.MapTo<ApplicationUser, UserPopUpViewModel>();
                userViewModel.TestCaseInTestRunID = testCaseInTestRun.ID;
                usersPopUpViewModel.Add(userViewModel);
            }
            return usersPopUpViewModel;
        }

        public void AssignTestCaseToUser(int testCaseInTestRunId, string username)
        {

        }
        public void AssignTestCaseToMe(int testCaseInTestRunId)
        {

        }

        public void AssignTestCaseToMe(int? testCaseInTestRunID, string userId)
        {
            var user = UserManager.FindById(userId);
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new TestCaseNotFoundException("User was not found.");
            }
            if (!testCaseInTestRunID.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new TestCaseNotFoundException("Id was not valid.");
            }
            var testCaseInTestRunDataModel = testCasesInTestRunRepository.GetTestCaseInTestRunByID(testCaseInTestRunID.Value);
            CheckExceptionTestCaseInTestRun(testCaseInTestRunDataModel);
            var assignedTestCaseInTestRun = new EditTestCasesInTestRunViewModel
            {
                AssignedBy = userId,
                AssignedTo = userId,
                LastModified = user.Email,
                LastModifiedDate = DateTime.Now
            };
            testCaseInTestRunDataModel = assignedTestCaseInTestRun.MapTo<EditTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>(testCaseInTestRunDataModel);
            testCasesInTestRunRepository.AssignTestCaseToUser(testCaseInTestRunDataModel);
            testCasesInTestRunRepository.Save();
        }

        

        public void AssignTestCaseToUser(UserPopUpViewModel userBeAssign)
        {
            var assignedBy = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (userBeAssign == null)
            {
                logger.Error("User was not found.");
                throw new TestCaseNotFoundException("User was not found.");
            }
            if (assignedBy == null)
            {
                logger.Error("User was not found.");
                throw new TestCaseNotFoundException("User was not found.");
            }
            var testCaseInTestRunDataModel = testCasesInTestRunRepository.GetTestCaseInTestRunByID(userBeAssign.TestCaseInTestRunID);
            CheckExceptionTestCaseInTestRun(testCaseInTestRunDataModel);
            var assignedTestCaseInTestRun = new EditTestCasesInTestRunViewModel
            {
                AssignedBy = assignedBy.Id,
                AssignedTo = userBeAssign.ID,
                LastModified = assignedBy.Email,
                LastModifiedDate = DateTime.Now
            };
            testCaseInTestRunDataModel = assignedTestCaseInTestRun.MapTo<EditTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>(testCaseInTestRunDataModel);
            testCasesInTestRunRepository.AssignTestCaseToUser(testCaseInTestRunDataModel);
            testCasesInTestRunRepository.Save();
        }

        private void CheckExceptionTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            if (testCaseInTestRun == null)
            {
                logger.Error("The test case in the current test run was not found.");
                throw new TestCaseInTestRunNotFoundException("The test case in the current test run was not found.");
            }
            var testSuite = testSuiteRepository.GetTestSuiteByID(testCaseInTestRun.TestSuiteID);
            var testCase = testCaseRepository.GetTestCaseByID(testCaseInTestRun.TestCaseID);
            if (testCase == null)
            {
                logger.Error("Test case was not found.");
                throw new TestCaseNotFoundException("Test case was not found.");
            }
            var testRun = testRunRepository.GetTestRunByID(testCaseInTestRun.TestRunID);
            if (testRun == null)
            {
                logger.Error("Test run was not found.");
                throw new TestRunNotFoundException("Test run was not found.");
            }
        }

        public IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            var list = testCasesInTestRunRepository.GetPagedListTestCasesByTestRun(testRunId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>();
            return mappedList;
        }
    }
}
#endregion


