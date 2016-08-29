using AutoMapper;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCaseInTestSuitePopUpMappingProfile : Profile
    {
       // private ITestCasesInTestRunRepository testCasesInTestRunRepository;

        public TestCaseInTestSuitePopUpMappingProfile(string profileName) : base(profileName)
        {
       //     testCasesInTestRunRepository = new TestCasesInTestRunRepository();

            this.CreateMap<TestCaseDataModel, TestCaseInTestSuitePopUpViewModel>();
            this.CreateMap<TestSuiteDataModel, TestSuiteInTestRunPopUpViewModel>();
            this.CreateMap<ApplicationUser, UserPopUpViewModel>();
        }

    }
}
