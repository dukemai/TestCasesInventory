using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    class RoleMappingProfile : Profile
    {
        protected RoleManager<IdentityRole> RoleManager;
        protected UserManager<ApplicationUser> UserManager;

        public RoleMappingProfile(string profileName) : base(profileName)
        {
            var userRoleRepository = new UserRoleRepository();

            this.CreateMap<IdentityRole, RoleViewModel>()
                 .ForMember(dest => dest.numberOfAccount, opt => opt.MapFrom(src => userRoleRepository.NumberAcountInRole(src.Id)));
            this.CreateMap<IPagedList<IdentityRole>, IPagedList<RoleViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<IdentityRole, RoleViewModel>>();
            this.CreateMap<ApplicationUser, UsersNotBelongRoleViewModel>();
            this.CreateMap<ApplicationUser, UsersBelongRoleViewModel>();
        }
    }
}
