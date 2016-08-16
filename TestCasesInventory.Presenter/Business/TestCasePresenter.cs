using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCasePresenter : ITestCasePresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestCaseRepository testCaseRepository;
        protected ITestSuiteRepository testSuiteRepository;
        protected ApplicationUserManager UserManager;

        public TestCasePresenter(HttpContextBase context)
        {
            HttpContext = context;
            testCaseRepository = new TestCaseRepository();
            testSuiteRepository = new TestSuiteRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public TestCaseViewModel GetTestCaseById(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("TestCase Id was not valid.");
            }
            var testCase = testCaseRepository.GetTestCaseByID(id.Value);
            if (testCase == null)
            {
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            var testSuite = testSuiteRepository.GetTestSuiteByID(testCase.TestSuiteID);
            if (testSuite == null)
            {
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            var testCaseViewModel = testCase.MapTo<TestCaseDataModel, TestCaseViewModel>();
            return testCaseViewModel;
        }

        public void InsertTestCase(CreateTestCaseViewModel testCase)
        {

            var testCaseDataModel = testCase.MapTo<CreateTestCaseViewModel, TestCaseDataModel>();
            testCaseRepository.InsertTestCase(testCaseDataModel);
            testCaseRepository.Save();
        }

        public void UpdateTestCase(int id, EditTestCaseViewModel testCase)
        {

            var testCaseDataModel = testCaseRepository.GetTestCaseByID(id);
            if (testCaseDataModel == null)
            {
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                testCaseDataModel = testCase.MapTo<EditTestCaseViewModel, TestCaseDataModel>(testCaseDataModel);
            };
            testCaseRepository.UpdateTestCase(testCaseDataModel);
            testCaseRepository.Save();
        }

        public void DeleteTestCase(int id)
        {
            var testCaseDataModel = testCaseRepository.GetTestCaseByID(id);
            if (testCaseDataModel == null)
            {
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                testCaseRepository.DeleteTestCase(id);
                testCaseRepository.Save();
            }
        }

        public IPagedList<TestCaseViewModel> GetTestCasesForTestSuite(int testSuiteId, FilterOptions filterOptions)
        {
            var list = testCaseRepository.GetTestCasesForTestSuite(testSuiteId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCaseDataModel>, IPagedList<TestCaseViewModel>>();
            return mappedList;
        }
    }
}
