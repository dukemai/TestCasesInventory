using AutoMapper;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCaseInStorageMappingProfile : Profile
    {
       // private ITestCasesInTestRunRepository testCasesInTestRunRepository;

        public TestCaseInStorageMappingProfile(string profileName) : base(profileName)
        {
       //     testCasesInTestRunRepository = new TestCasesInTestRunRepository();

            this.CreateMap<TestCaseDataModel, TestCaseInStorageViewModel>();
        }

    }
}
