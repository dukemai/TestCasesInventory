using System;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;

namespace TestCasesInventory.Presenter.Synchroniser
{
    public class UsersObserver : IObserver<ApplicationUser>
    {
        ITestSuiteRepository testSuiteRepository;

        public UsersObserver()
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

        public void OnNext(ApplicationUser value)
        {
            BackgroundSynchroniser.Instance.AddNewTask(new BackgroundTask<ApplicationUser> { Action = () => { UpdateTestSuite(value); } });
        }

        protected void UpdateTestSuite(ApplicationUser user)
        {
            if (user.TeamID.HasValue)
            {
                var testSuites = testSuiteRepository.GetTestSuitesForUser(user.UserName);
                var isUpdate = false;
                foreach (var testSuite in testSuites)
                {
                    if (user.UserName == testSuite.Created)
                    {
                        testSuite.CreateDisplayOnly = user.DisplayName;
                        isUpdate = true;
                    }
                    if (user.UserName == testSuite.LastModified)
                    {
                        testSuite.LastModifiedDisplayOnly = user.DisplayName;
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
}
