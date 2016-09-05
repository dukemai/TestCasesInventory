using AutoMapper;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCaseInTestSuitePopUpMappingProfile : Profile
    {
       // private ITestCasesInTestRunRepository testCasesInTestRunRepository;
       private TestCaseRepository testCaseRepository;

        public TestCaseInTestSuitePopUpMappingProfile(string profileName) : base(profileName)
        {
            //     testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            testCaseRepository = new TestCaseRepository();

            this.CreateMap<TestCaseDataModel, TestCaseInTestSuitePopUpViewModel>();
            this.CreateMap<TestSuiteDataModel, TestSuiteInTestRunPopUpViewModel>();
            this.CreateMap<ApplicationUser, UserPopUpViewModel>();
        }

    }
}
