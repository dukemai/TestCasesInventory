using Microsoft.AspNet.Identity.EntityFramework;
using System;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;

namespace TestCasesInventory.Presenter.Synchroniser
{
    public class TestSuiteObserver : IObserver<TestSuiteDataModel>
    {
        ITestSuiteRepository testSuiteRepository;
        ITeamRepository teamRepository;

        public TestSuiteObserver()
        {
            testSuiteRepository = new TestSuiteRepository();
            teamRepository = new TeamRepository();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(TestSuiteDataModel value)
        {
            BackgroundSynchroniser.Instance.AddNewTask(new BackgroundTask<TestSuiteDataModel> { Action = () => { UpdateTestSuite(value); } });
        }

        protected void UpdateTestSuite(TestSuiteDataModel testSuite)
        {
            var isUpdate = false;
            var dataModel = testSuiteRepository.GetTestSuiteByID(testSuite.ID);
            if (dataModel == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(dataModel.TeamNameDisplayOnly))
            {
                var team = teamRepository.GetTeamByID(testSuite.TeamID);
                if (team != null)
                {
                    dataModel.TeamNameDisplayOnly = team.Name;
                    isUpdate = true;
                }
            }
            if (string.IsNullOrEmpty(dataModel.CreateDisplayOnly))
            {
                var user = teamRepository.FindUserByEmail(testSuite.Created);
                if (user != null)
                {
                    dataModel.CreateDisplayOnly = user.DisplayName;
                    isUpdate = true;
                }
            }
            if (string.IsNullOrEmpty(dataModel.LastModifiedDisplayOnly))
            {
                var user = teamRepository.FindUserByEmail(testSuite.LastModified);
                if (user != null)
                {
                    dataModel.LastModifiedDisplayOnly = user.DisplayName;
                    isUpdate = true;
                }
            }
            if (isUpdate)
            {
                testSuiteRepository.Save();
            }

        }
    }
}
