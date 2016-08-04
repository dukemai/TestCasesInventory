using System;
using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;
using System.Linq;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCasePresenter : ITestCasePresenter
    {
        protected ITestCaseRepository TestCaseRepository;
        public TestCasePresenter()
        {
            TestCaseRepository = new TestCaseRepository();
        }

        public List<TestCaseViewModel> ListAll(int? testSuiteID)
        {
            if (!testSuiteID.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var ListTestCase = TestCaseRepository.ListAll(testSuiteID.Value);
            List<TestCaseViewModel> ListAllTestCase = new List<TestCaseViewModel>();
            foreach(var item in ListTestCase)
            {
                var TestCase = new TestCaseViewModel
                {
                    ID = item.ID,
                    Title = item.Title,
                    TestSuiteID = item.TestSuiteID,
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
                throw new Exception("Id was not valid.");
            }
            var testCase = TestCaseRepository.GetTestCaseByID(id.Value);
            if (testCase == null)
            {
                throw new TeamNotFoundException("Team was not found.");
            }
            return new TestCaseViewModel
            {
                ID = testCase.ID,
                Title = testCase.Title,
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
                Precondition = testCase.Precondition,
                Attachment = testCase.Attachment,
                Expect = testCase.Expect,
                Created = testCase.Created,
                LastModified = testCase.LastModified,
                CreatedDate = testCase.CreatedDate,
                LastModifiedDate = testCase.LastModifiedDate
            };
            TestCaseRepository.InsertTestCase(testCaseDataModel);
            TestCaseRepository.Save();
        }

        public void UpdateTestCase(int id, EditTestCaseViewModel testCase)
        {

            var testCaseDataModel = TestCaseRepository.GetTestCaseByID(id);
            if(testCaseDataModel == null)
            {
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                testCaseDataModel.Title = testCase.Title;
                testCaseDataModel.Description = testCase.Description;
                testCaseDataModel.Precondition = testCase.Precondition;
                testCaseDataModel.Attachment = testCase.Attachment;
                testCaseDataModel.Expect = testCase.Expect;
                testCaseDataModel.Created = testCase.Created;
                testCaseDataModel.LastModified = testCase.LastModified;
                testCaseDataModel.CreatedDate = testCase.CreatedDate;
                testCaseDataModel.LastModifiedDate = testCase.LastModifiedDate;
            };
            TestCaseRepository.UpdateTestCase(testCaseDataModel);
            TestCaseRepository.Save();
        }

        public void DeleteTestCase(int id)
        {
            var testCaseDataModel = TestCaseRepository.GetTestCaseByID(id);
            if(testCaseDataModel == null)
            {
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                TestCaseRepository.DeleteTestCase(id);
                TestCaseRepository.Save();
            }
        }
    }
}
