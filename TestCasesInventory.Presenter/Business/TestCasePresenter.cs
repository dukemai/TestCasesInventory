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
            var createdBy = UserManager.FindByEmail(testCase.Created);
            return new TestCaseViewModel
            {
                ID = testCase.ID,
                Priority = testCase.Priority,
                Title = testCase.Title,
                TestSuiteID = testCase.TestSuiteID,
                TestSuiteTitle = testSuite.Title,
                Description = testCase.Description,
                Precondition = testCase.Precondition,
                Attachment = testCase.Attachment,
                Expect = testCase.Expect,
                Created = createdBy != null ? createdBy.DisplayName : string.Empty,
                LastModified = createdBy != null ? createdBy.DisplayName : string.Empty,
                CreatedDate = testCase.CreatedDate,
                LastModifiedDate = testCase.LastModifiedDate
            };
        }

        public void InsertTestCase(CreateTestCaseViewModel testCase)
        {

            var testCaseDataModel = new TestCaseDataModel
            {
                Title = testCase.Title,
                Priority = testCase.Priority,
                Description = testCase.Description,
                TestSuiteID = testCase.TestSuiteID,
                Precondition = testCase.Precondition,
                Attachment = testCase.Attachment,
                Expect = testCase.Expect,
                Created = testCase.Created,
                LastModified = testCase.LastModified,
                CreatedDate = testCase.CreatedDate,
                LastModifiedDate = testCase.LastModifiedDate
            };
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
                testCaseDataModel.Title = testCase.Title;
                testCaseDataModel.Priority = testCase.Priority;
                testCaseDataModel.Description = testCase.Description;
                testCaseDataModel.Precondition = testCase.Precondition;
                testCaseDataModel.Expect = testCase.Expect;
                testCaseDataModel.LastModified = testCase.LastModified;
                testCaseDataModel.LastModifiedDate = testCase.LastModifiedDate;
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
            var mappedList = Mapper.Map<IPagedList<TestCaseViewModel>>(list);
            return mappedList;
        }
    }
}
