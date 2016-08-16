using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Web;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class TestSuitePresenter : PresenterBase, ITestSuitePresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestSuiteRepository testSuiteRepository;
        protected ApplicationUserManager UserManager;
        protected ITeamRepository teamRepository;
        protected ITestCaseRepository testCaseRepository;
        protected RoleManager<IdentityRole> RoleManager;


        public TestSuitePresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            testSuiteRepository = new TestSuiteRepository();
            teamRepository = new TeamRepository();
            testCaseRepository = new TestCaseRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        }

        public TestSuiteViewModel GetTestSuiteById(int? testSuiteID)
        {
            if (!testSuiteID.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var testSuite = testSuiteRepository.GetTestSuiteByID(testSuiteID.Value);
            if (testSuite == null)
            {
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            var testSuiteViewModel = Mapper.Map<TestSuiteViewModel>(testSuite);
            return testSuiteViewModel;
        }

        public void InsertTestSuite(CreateTestSuiteViewModel testSuite)
        {
            var teamID = UserManager.FindByEmail(testSuite.Created).TeamID;
            if (!teamID.HasValue)
            {
                throw new Exception("User has not been assigned to any team.");
            }
            var testSuiteDataModel = new TestSuiteDataModel
            {
                Title = testSuite.Title,
                TeamID = teamID.Value,
                Description = testSuite.Description,
                Created = testSuite.Created,
                CreatedDate = testSuite.CreatedDate,
                LastModified = testSuite.LastModified,
                LastModifiedDate = testSuite.LastModifiedDate
            };
            testSuiteRepository.InsertTestSuite(testSuiteDataModel);
            testSuiteRepository.Save();
        }

        public void UpdateTestSuite(int testSuiteID, EditTestSuiteViewModel testSuite)
        {
            var testSuiteDataModel = testSuiteRepository.GetTestSuiteByID(testSuiteID);
            if (testSuiteDataModel == null)
            {
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            else
            {
                testSuiteDataModel.Title = testSuite.Title;
                testSuiteDataModel.Description = testSuite.Description;
                testSuiteDataModel.LastModified = testSuite.LastModified;
                testSuiteDataModel.LastModifiedDate = testSuite.LastModifiedDate;
                testSuiteRepository.UpdateTestSuite(testSuiteDataModel);
                testSuiteRepository.Save();
            }
        }

        public void DeleteTestSuite(int testSuiteID)
        {
            var testSuiteDataModel = testSuiteRepository.GetTestSuiteByID(testSuiteID);
            if (testSuiteDataModel == null)
            {
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            else
            {
                var testCasesForTestSuite = testCaseRepository.ListAll(testSuiteID);
                foreach (var testCase in testCasesForTestSuite)
                {
                    testCaseRepository.DeleteTestCase(testCase.ID);
                    testCaseRepository.Save();
                }
                testSuiteRepository.DeleteTestSuite(testSuiteID);
                testSuiteRepository.Save();
            }
        }

        public IPagedList<TestSuiteViewModel> GetTestSuites(FilterOptions options, string userId)
        {
            var user = UserManager.FindById(userId);
            var getAll = UserManager.IsInRole(user.Id, PrivilegedUsersConfig.AdminRole);
            var list = testSuiteRepository.GetTestSuites(options, user.TeamID, getAll);
            var mappedList = list.MapTo<IPagedList<TestSuiteDataModel>, IPagedList<TestSuiteViewModel>>();
            return mappedList;
        }
    }
}
