﻿using AutoMapper;
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
    public class TestRunPresenter : PresenterBase , ITestRunPresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestRunRepository testRunRepository;
        protected ApplicationUserManager UserManager;
        protected ITeamRepository teamRepository;
        protected ITestCaseRepository testCaseRepository;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected RoleManager<IdentityRole> RoleManager;

        public TestRunPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            testRunRepository = new TestRunRepository();
            teamRepository = new TeamRepository();
            testCaseRepository = new TestCaseRepository();
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
            var teamID = UserManager.FindByEmail(testRun.Created).TeamID;
            testRun.TeamID = teamID.Value;
            if (!teamID.HasValue)
            {
                logger.Error("User has not been assigned to any team.");
                throw new Exception("User has not been assigned to any team.");
            }
            var testRunDataModel = testRun.MapTo<CreateTestRunViewModel, TestRunDataModel>();
            testRunRepository.InsertTestRun(testRunDataModel);
            testRunRepository.Save();
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
                //var testCasesForTestRun = testCasesInTestRunRepository.ListAll(testRunID);
                //foreach (var testCase in testCasesForTestRun)
                //{
                //    testCaseRepository.DeleteTestCase(testCase.ID);
                //    testCaseRepository.Save();
                //}
                testRunRepository.DeleteTestRun(testRunID);
                testRunRepository.Save();
            }
        }


        public IPagedList<TestRunViewModel> GetTestRuns(FilterOptions options, string userId)
        {
            var user = UserManager.FindById(userId);
            var getAll = UserManager.IsInRole(user.Id, PrivilegedUsersConfig.AdminRole);
            var list = testRunRepository.GetTestRuns(options, user.TeamID, getAll);
            var mappedList = list.MapTo<IPagedList<TestRunDataModel>, IPagedList<TestRunViewModel>>();
            return mappedList;
        }

    }

  
}