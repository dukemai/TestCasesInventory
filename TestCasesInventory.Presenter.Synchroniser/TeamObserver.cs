using System;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;

namespace TestCasesInventory.Presenter.Synchroniser
{
    public class TeamObserver : IObserver<TeamDataModel>
    {
        ITestSuiteRepository testSuiteRepository;

        public TeamObserver()
        {
            testSuiteRepository = new TestSuiteRepository();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(TeamDataModel value)
        {
            BackgroundSynchroniser.Instance.AddNewTask(new BackgroundTask<ApplicationUser> { Action = () => { UpdateTestSuite(value); } });
        }

        protected void UpdateTestSuite(TeamDataModel team)
        {
            var testSuites = testSuiteRepository.GetTestSuitesForTeam(team.ID);
            var isUpdate = false;
            foreach (var testSuite in testSuites)
            {
                if (!string.Equals(testSuite.TeamNameDisplayOnly, team.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    testSuite.TeamNameDisplayOnly = team.Name;
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
