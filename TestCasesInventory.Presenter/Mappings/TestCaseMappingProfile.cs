﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCaseMappingProfile : Profile
    {
        UserManager<ApplicationUser> UserManager;

        public TestCaseMappingProfile(string profileName) : base(profileName)
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<TestCaseDataModel, TestCaseViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                 .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName));
            this.CreateMap<IPagedList<TestCaseDataModel>, IPagedList<TestCaseViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestCaseDataModel, TestCaseViewModel>>();
            this.CreateMap<TestCaseDataModel, CreateTestCaseViewModel>();
            this.CreateMap<CreateTestCaseViewModel, TestCaseDataModel>();
            this.CreateMap<TestCaseDataModel, EditTestCaseViewModel>();
            this.CreateMap<EditTestCaseViewModel, TestCaseDataModel>()
                .Ignore(m => m.Created)
                .Ignore(m => m.CreatedDate)
                .Ignore(m => m.TestSuiteID);
        }
    }
}
