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
            if (string.IsNullOrEmpty(testSuite.TeamNameDisplayOnly))
            {
                var team = teamRepository.GetTeamByID(testSuite.TeamID);
                if (team != null)
                {
                    testSuite.TeamNameDisplayOnly = team.Name;
                    isUpdate = true;
                }
            }
            if (string.IsNullOrEmpty(testSuite.CreateDisplayOnly))
            {
                var user = teamRepository.FindUserByID(testSuite.Created);
                if (user != null)
                {
                    testSuite.TeamNameDisplayOnly = user.DisplayName;
                    isUpdate = true;
                }
            }
            if (string.IsNullOrEmpty(testSuite.LastModifiedDisplayOnly))
            {
                var user = teamRepository.FindUserByID(testSuite.LastModifiedDisplayOnly);
                if (user != null)
                {
                    testSuite.TeamNameDisplayOnly = user.DisplayName;
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
