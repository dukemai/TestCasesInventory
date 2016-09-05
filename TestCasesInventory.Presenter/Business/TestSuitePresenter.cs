﻿using AutoMapper;
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
    public class TestSuitePresenter : ObservablePresenterBase<TestSuiteDataModel>, ITestSuitePresenter
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

        public TestSuiteViewModel GetTestSuiteById(int testSuiteID)
        {
            var testSuite = testSuiteRepository.GetTestSuiteByID(testSuiteID);
            if (testSuite == null)
            {
                logger.Error("Test Suite was not found.");
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            var testSuiteViewModel = testSuite.MapTo<TestSuiteDataModel, TestSuiteViewModel>();
            return testSuiteViewModel;
        }

        public void InsertTestSuite(CreateTestSuiteViewModel testSuite)
        {
            //var user = UserManager.FindByEmail(testSuite.Created);
            //if (UserManager.IsInRole(user.Id, PrivilegedUsersConfig.AdminRole))
            //{
            //    testSuite.TeamID = testSuite.TeamID;
            //}
            //else
            //{
            //    testSuite.TeamID = user.TeamID;
            //}

            if (!testSuite.TeamID.HasValue)
            {
                logger.Error("User has not been assigned to any team.");
                throw new Exception("User has not been assigned to any team.");
            }
            var testSuiteDataModel = testSuite.MapTo<CreateTestSuiteViewModel, TestSuiteDataModel>();

            testSuiteRepository.InsertTestSuite(testSuiteDataModel);
            testSuiteRepository.Save();
            FeedObservers(testSuiteDataModel);
        }

        public void UpdateTestSuite(int testSuiteID, EditTestSuiteViewModel testSuite)
        {
            var testSuiteDataModel = testSuiteRepository.GetTestSuiteByID(testSuiteID);
            if (testSuiteDataModel == null)
            {
                logger.Error("Test Suite was not found.");
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            else
            {                              
                testSuiteDataModel = testSuite.MapTo<EditTestSuiteViewModel, TestSuiteDataModel>(testSuiteDataModel);
                testSuiteRepository.UpdateTestSuite(testSuiteDataModel);
                testSuiteRepository.Save();
            }
        }

        public void DeleteTestSuite(int testSuiteID)
        {
            var testSuiteDataModel = testSuiteRepository.GetTestSuiteByID(testSuiteID);
            if (testSuiteDataModel == null)
            {
                logger.Error("Test Suite was not found.");
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

        public CreateTestSuiteViewModel GetTestSuiteForCreate()
        {
            return new CreateTestSuiteViewModel();
        }

        public EditTestSuiteViewModel GetTestSuiteForEdit(int testSuiteID)
        {
            var testSuite = testSuiteRepository.GetTestSuiteByID(testSuiteID);
            if (testSuite == null)
            {
                logger.Error("Test Suite was not found.");
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            var testSuiteViewModel = testSuite.MapTo<TestSuiteDataModel, EditTestSuiteViewModel>();
            return testSuiteViewModel;
        }

        public CreateTestSuiteViewModel GetTestSuiteForAdminCreate(int teamID)
        {
            var model = GetTestSuiteForCreate();
            model.TeamID = teamID;
            model.Teams = teamRepository.ListAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.ID.ToString()
            }).ToList();

            return model;
        }

        public EditTestSuiteViewModel GetTestSuiteForAdminEdit(int testSuiteID)
        {
            var model = GetTestSuiteForEdit(testSuiteID);
            
            model.Teams = teamRepository.ListAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.ID.ToString()
            }).ToList();

            return model;
        }
    }
}
