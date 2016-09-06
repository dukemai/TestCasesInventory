﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System.Linq;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    class UserMappingProfile : Profile
    {

        public UserMappingProfile(string profileName) : base(profileName)
        {
            this.CreateMap<ApplicationUser, UsersBelongTeamViewModel>();
            this.CreateMap<ApplicationUser, UsersNotBelongTeamViewModel>();
            this.CreateMap<IPagedList<ApplicationUser>, IPagedList<UsersBelongTeamViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<ApplicationUser, UsersBelongTeamViewModel>>();
            this.CreateMap<IPagedList<ApplicationUser>, IPagedList<UsersNotBelongTeamViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<ApplicationUser, UsersNotBelongTeamViewModel>>();
            this.CreateMap<IPagedList<ApplicationUser>, IPagedList<UsersBelongRoleViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<ApplicationUser, UsersBelongRoleViewModel>>();
            this.CreateMap<IPagedList<ApplicationUser>, IPagedList<UsersNotBelongRoleViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<ApplicationUser, UsersNotBelongRoleViewModel>>();
        }
    }
}
