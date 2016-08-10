using System;
using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;
using System.Linq;
using TestCasesInventory.Presenter.Config;
using System.IO;
using Microsoft.AspNet.Identity;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCasePresenter : ITestCasePresenter
    {
        protected ITestCaseRepository testCaseRepository;
        protected ITestSuiteRepository testSuiteRepository;

        public TestCasePresenter()
        {
            testCaseRepository = new TestCaseRepository();
            testSuiteRepository = new TestSuiteRepository();
        }

        public List<TestCaseViewModel> ListAll(int? testSuiteID)
        {
            if (!testSuiteID.HasValue)
            {
                throw new Exception("TestSuite ID was not valid.");
            }
            var testSuite = testSuiteRepository.GetTestSuiteByID(testSuiteID.Value);
            if(testSuite == null)
            {
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            var ListTestCase = testCaseRepository.ListAll(testSuiteID.Value);
            List<TestCaseViewModel> ListAllTestCase = new List<TestCaseViewModel>();
            foreach(var item in ListTestCase)
            {
                var TestCase = new TestCaseViewModel
                {
                    ID = item.ID,
                    Title = item.Title,
                    TestSuiteID = item.TestSuiteID,
                    TestSuiteTitle = testSuite.Title,
                    Description = item.Description,
                    Precondition = item.Precondition,
                    Attachment = item.Attachment,
                    Expect = item.Expect,
                    Created = item.Created,
                    LastModified = item.LastModified,
                    CreatedDate = item.CreatedDate,
                    LastModifiedDate = item.LastModifiedDate
                };
                ListAllTestCase.Add(TestCase);
            }
            return ListAllTestCase;
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
            var testSuiteTitle = testSuiteRepository.GetTestSuiteByID(testCase.TestSuiteID).Title;
            return new TestCaseViewModel
            {
                ID = testCase.ID,
                Title = testCase.Title,
                TestSuiteID = testCase.TestSuiteID,
                TestSuiteTitle = testSuiteTitle,
                Description = testCase.Description,
                Precondition = testCase.Precondition,
                Attachment = testCase.Attachment,
                Expect = testCase.Expect,
                Created = testCase.Created,
                LastModified = testCase.LastModified,
                CreatedDate = testCase.CreatedDate,
                LastModifiedDate = testCase.LastModifiedDate
            };
        }
        
        public void InsertTestCase(CreateTestCaseViewModel testCase)
        {

            var testCaseDataModel = new TestCaseDataModel
            {
                Title = testCase.Title,
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
            testCase.ID = testCaseDataModel.ID;
        }

        public void UpdateTestCase(int id, EditTestCaseViewModel testCase)
        {

            var testCaseDataModel = testCaseRepository.GetTestCaseByID(id);
            if(testCaseDataModel == null)
            {
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                testCaseDataModel.Title = testCase.Title;
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
            if(testCaseDataModel == null)
            {
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                testCaseRepository.DeleteTestCase(id);
                testCaseRepository.Save();
            }
        }

        public string GetFileUrl(string id)
        {
            var UrlPath = Path.Combine(UserConfigurations.TestCasesFolderPath, id);
            return UrlPath;
        }
    }
}
