﻿using System.Web.Mvc;
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
        #region Properties
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
        #endregion

        #region TestRunResult
        public TestRunResultViewModel GetTestRunResultById(int testRunResultID)
        {
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            CheckExceptionTestRunResult(testRunResult);
            var testRunResultViewModel = testRunResult.MapTo<TestRunResultDataModel, TestRunResultViewModel>();
            return testRunResultViewModel;
        }

        public int CreateTestRunResult(int testRunID)
        {
            var user = UserManager.FindById(HttpContext.User.Identity.GetUserId());
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new TestRunNotFoundException("User was not found.");
            }
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunResultNotFoundException("Test Run was not found");
            }
            var testRunResultViewModel = new CreateTestRunResultViewModel
            {
                TestRunID = testRunID,
                Status = TestRunResultStatus.InProgress,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Created = user.Email,
                LastModified = user.Email,
            };
            var testRunResultDataModel = testRunResultViewModel.MapTo<CreateTestRunResultViewModel, TestRunResultDataModel>();
            testRunResultRepository.InsertTestRunResult(testRunResultDataModel);
            testRunResultRepository.Save();

            var testRunResultID = testRunResultDataModel.ID;
            return testRunResultID;
        }

        public void DeleteTestRunResult(int testRunResultID)
        {
            var testRunResultDataModel = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            CheckExceptionTestRunResult(testRunResultDataModel);
            testRunResultRepository.DeleteTestRunResult(testRunResultID);
            testRunResultRepository.Save();
        }

        public IPagedList<TestRunResultViewModel> GetTestRunResults(FilterOptions options, int testRunID)
        {
            var list = testRunResultRepository.GetTestRunResults(options, testRunID);
            var mappedList = list.MapTo<IPagedList<TestRunResultDataModel>, IPagedList<TestRunResultViewModel>>();
            return mappedList;
        }

        public void FinishTestRunResult(int testRunResultId)
        {
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultId);
            CheckExceptionTestRunResult(testRunResult);
            testRunResult.Status = TestRunResultStatus.Finished;
            testRunResultRepository.UpdateTestRunResult(testRunResult);
            testRunResultRepository.Save();
        }

        #endregion

        #region Test Cases
        public List<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToUser(int testRunID, string userName)
        {
            var user = UserManager.FindByEmail(userName);
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new UserNotFoundException("User was not found.");
            }

            var listTestCasesInTestRunResult = new List<TestCasesInTestRunResultViewModel>();
            var listTestCasesInTestRunResultDataModel = testCasesInTestRunRepository.GetTestCasesInTestRunAssignedToUser(testRunID, user.Id);
            foreach (var testCaseInTestRun in listTestCasesInTestRunResultDataModel)
            {
                listTestCasesInTestRunResult.Add(testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>());
            }
            return listTestCasesInTestRunResult;
        }
        public List<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToMe(int testRunId)
        {
            var userName = UserManager.FindById(HttpContext.User.Identity.GetUserId()).Email;
            return GetTestCasesAssignedToUser(testRunId, userName);
        }

        public List<TestCasesInTestRunResultViewModel> GetAllTestCases(int testRunId)
        {
            var listTestCasesInTestRunResult = new List<TestCasesInTestRunResultViewModel>();
            var listTestCasesInTestRunDataModel = testCasesInTestRunRepository.GetTestCasesInTestRun(testRunId);
            foreach (var testCase in listTestCasesInTestRunDataModel)
            {
                listTestCasesInTestRunResult.Add(testCase.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>());
            }
            return listTestCasesInTestRunResult;
        }
        public List<TestCasesInTestRunResultViewModel> GetSelectedTestCases(int testRunId, List<int> selectedTestCases)
        {
            var listTestCasesInTestRunResult = new List<TestCasesInTestRunResultViewModel>();
            foreach (var id in selectedTestCases)
            {
                var testCase = testCasesInTestRunRepository.GetTestCaseInTestRunByID(id);
                listTestCasesInTestRunResult.Add(testCase.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>());
            }
            return listTestCasesInTestRunResult;
        }

        private void CheckExceptionTestRunResult(TestRunResultDataModel testRunResult)
        {
            if (testRunResult == null)
            {
                logger.Error("Test Run Result was not found.");
                throw new TestRunResultNotFoundException("Test Run Result was not found");
            }
            var testRun = testRunRepository.GetTestRunByID(testRunResult.TestRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunResultNotFoundException("Test Run was not found");
            }
        }
        #endregion
    }
}
