using Microsoft.AspNet.Identity;
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
        protected HttpContextBase HttpContext;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected ITestSuiteRepository testSuiteRepository;
        protected ITestCaseRepository testCaseRepository;
        protected ITestRunRepository testRunRepository;
        protected ITeamRepository teamRepository;
        protected ApplicationUserManager UserManager;




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

        public TestCasesInTestRunViewModel GetTestCaseInTestRunById(int? id)
        {
            if (!id.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new Exception("Id was not valid.");
            }
            var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(id.Value);
            CheckExceptionTestCaseInTestRun(testCaseInTestRun);
            var testCaseInTestRunViewModel = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>();
            return testCaseInTestRunViewModel;
        }

        public void AddTestCasesToTestRun(List<TestCaseInTestSuitePopUpViewModel> listTestCasesPopUp, int testRunID)
        {
            var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new UserNotFoundException("User was not found.");
            }
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            var testSuiteDataModel = testSuiteRepository.GetTestSuiteByID(listTestCasesPopUp.First().TestSuiteID);
            if (testSuiteDataModel == null)
            {
                logger.Error("Test Suite with id = " + listTestCasesPopUp.First().TestSuiteID + " was not found.");
                throw new TestCaseNotFoundException("Test Suite with id = " + listTestCasesPopUp.First().TestSuiteID + " was not found.");
            }
            foreach (var testCasePopUp in listTestCasesPopUp)
            {
                var testCaseDataModel = testCaseRepository.GetTestCaseByID(testCasePopUp.ID);

                if (testCasePopUp.Checked)
                {
                    if (testCaseDataModel == null)
                    {
                        logger.Error("Test Case with id = " + testCasePopUp.ID + " was not found.");
                        throw new TestCaseNotFoundException("Test Case with id = " + testCasePopUp.ID + " was not found.");
                    }
                    var testCaseAlreadyInTestRun = testCasesInTestRunRepository.TestCaseAlreadyInTestRun(testRunID, testCasePopUp.ID);
                    if (!testCaseAlreadyInTestRun.Any())
                    {
                        var testCaseInTestRunViewModel = new CreateTestCasesInTestRunViewModel
                        {
                            TestCaseID = testCasePopUp.ID,
                            TestRunID = testRunID,
                            TestSuiteID = testCasePopUp.TestSuiteID,
                            CreatedDate = DateTime.Now,
                            LastModifiedDate = DateTime.Now,
                            Created = user.Email,
                            LastModified = user.Email,
                            AssignedTo = user.Id,
                            AssignedBy = user.Id,
                        };
                        var testCaseInTestRunDataModel = testCaseInTestRunViewModel.MapTo<CreateTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>();
                        testCasesInTestRunRepository.InsertTestCaseInTestRun(testCaseInTestRunDataModel);
                        testCasesInTestRunRepository.Save();
                    }
                }

                else
                {
                    if (testCaseDataModel != null)
                    {
                        var testCaseAlreadyInTestRun = testCasesInTestRunRepository.TestCaseAlreadyInTestRun(testRunID, testCasePopUp.ID);
                        if (testCaseAlreadyInTestRun.Any())
                        {
                            testCasesInTestRunRepository.DeleteTestCaseInTestRun(testCaseAlreadyInTestRun.First().ID);
                            testCasesInTestRunRepository.Save();
                        }
                    }
                }

            }
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
            testCasesInTestRunRepository.UpdateTestCaseInTestRun(testCaseInTestRunDataModel);
            testCasesInTestRunRepository.Save();
        }


        public IList<UsersBelongTeamViewModel> ListUsersAssignedToTestCase(int? testCaseInTestRunID)
        {
            if (!testCaseInTestRunID.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new Exception("Id was not valid.");
            }
            var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(testCaseInTestRunID.Value);
            CheckExceptionTestCaseInTestRun(testCaseInTestRun);
            var testRun = testRunRepository.GetTestRunByID(testCaseInTestRun.TestRunID);
            var listUsers = teamRepository.ListUsersByTeamID(testRun.TeamID);
            var listUsersViewModel = new List<UsersBelongTeamViewModel>();
            foreach (var user in listUsers)
            {
                var userViewModel = user.MapTo<ApplicationUser, UsersBelongTeamViewModel>();
                listUsersViewModel.Add(userViewModel);
            }
            return listUsersViewModel;
        }

        public void AssignTestCaseToUser(int? testCaseInTestRunID, UsersBelongTeamViewModel userBeAssign)
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
            if (!testCaseInTestRunID.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new TestCaseNotFoundException("Id was not valid.");
            }
            var testCaseInTestRunDataModel = testCasesInTestRunRepository.GetTestCaseInTestRunByID(testCaseInTestRunID.Value);
            CheckExceptionTestCaseInTestRun(testCaseInTestRunDataModel);
            CheckExceptionTestCaseInTestRun(testCaseInTestRunDataModel);
            var assignedTestCaseInTestRun = new EditTestCasesInTestRunViewModel
            {
                AssignedBy = assignedBy.Id,
                AssignedTo = userBeAssign.ID,
                LastModified = assignedBy.Email,
                LastModifiedDate = DateTime.Now
            };
            testCaseInTestRunDataModel = assignedTestCaseInTestRun.MapTo<EditTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>(testCaseInTestRunDataModel);
            testCasesInTestRunRepository.UpdateTestCaseInTestRun(testCaseInTestRunDataModel);
            testCasesInTestRunRepository.Save();
        }


        public void DeleteTestCaseInTestRun(int id)
        {
            var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(id);
            CheckExceptionTestCaseInTestRun(testCaseInTestRun);
            testCasesInTestRunRepository.DeleteTestCaseInTestRun(id);
            testCasesInTestRunRepository.Save();
        }

        public void CheckExceptionTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
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
            var list = testCasesInTestRunRepository.GetTestCasesByTestRunID(testRunId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>();
            return mappedList;
        }

    }
}
