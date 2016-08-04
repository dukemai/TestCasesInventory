using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using Microsoft.AspNet.Identity;

namespace TestCasesInventory.Presenter.Business
{
    public class TestSuitePresenter : PresenterBase, ITestSuitePresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestSuiteRepository testSuiteRepository;
        protected ApplicationUserManager UserManager;


        public TestSuitePresenter(HttpContextBase context):base()
        {
            HttpContext = context;
            testSuiteRepository = new TestSuiteRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
            return new TestSuiteViewModel
            {
                ID = testSuite.ID,
                Title = testSuite.Title,
                TeamID = testSuite.TeamID,
                Description = testSuite.Description,
                Created = testSuite.Created,
                CreatedDate = testSuite.CreatedDate,
                LastModified = testSuite.LastModified,
                LastModifiedDate = testSuite.LastModifiedDate
            };
        }

        public List<TestSuiteViewModel> ListAll()
        {
            var listTestSuite = testSuiteRepository.ListAll();
            List<TestSuiteViewModel> listTestSuiteView = new List<TestSuiteViewModel>();
            foreach (var item in listTestSuite)
            {
                var testSuiteView = new TestSuiteViewModel
                {
                    ID = item.ID,
                    Title = item.Title,
                    TeamID = item.TeamID,
                    Description = item.Description,
                    Created = item.Created,
                    CreatedDate = item.CreatedDate,
                    LastModified = item.LastModified,
                    LastModifiedDate = item.LastModifiedDate
                };
                listTestSuiteView.Add(testSuiteView);
            }
            return listTestSuiteView;
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
                testSuiteRepository.DeleteTestSuite(testSuiteID);
                testSuiteRepository.Save();
            }
        }

        
    }
}
