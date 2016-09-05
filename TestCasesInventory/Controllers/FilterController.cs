using System.Collections.Generic;
using System.Web.Mvc;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;
using System.Linq;
using TestCasesInventory.Web.Common.Utils;

namespace TestCasesInventory.Controllers
{
    public class FilterController : TestCasesInventory.Web.Common.Base.ControllerBase
    {
        public ActionResult FilterForListUserBelongTeam([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Name",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Mail",
                DisplayName = "Email",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Mail") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "Team",
                Action = "ListMembersInTeam",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForAddUserToTeam([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions, TabOptions tabOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Name",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Mail",
                DisplayName = "Email",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Mail") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "Team",
                Action = "AddUsersToTeam",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = tabOptions.ActiveTab == tabOptions.CurrentTabIndex ? filterOptions : PagingHelper.DefaultFilterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForRemoveUsersFromTeam([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions, TabOptions tabOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Name",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Mail",
                DisplayName = "Email",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Mail") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "Team",
                Action = "RemoveUsersFromTeam",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = tabOptions.ActiveTab == tabOptions.CurrentTabIndex ? filterOptions : PagingHelper.DefaultFilterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForTeam([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Team name",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "Team",
                Action = "Index",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForTestSuite([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Title",
                DisplayName = "Title",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Title") != null : true
            });

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Team",
                DisplayName = "Team",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Team") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "TestSuite",
                Action = "Index",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }
        public ActionResult FilterForTestRun([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Title",
                DisplayName = "Title",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Title") != null : true
            });

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Team",
                DisplayName = "Team",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Team") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "TestRun",
                Action = "Index",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForTestRunResult([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Status",
                DisplayName = "Status",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Status") != null : true
            });

       
            var viewModel = new FilterViewModel
            {
                Controller = "TestRunResult",
                Action = "Index",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForTestCase([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Title",
                DisplayName = "Title",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Title") != null : true
            });

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "CreatedBy",
                DisplayName = "Created By",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "CreatedBy") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "TestSuite",
                Action = "Details",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForTestCaseInTestRun([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Title",
                DisplayName = "Title",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Title") != null : true
            });

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "AssignedTo",
                DisplayName = "Assign To",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "AssignedTo") != null : true
            });

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "AssignedBy",
                DisplayName = "Assign By",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "AssignedBy") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "TestRun",
                Action = "Details",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }



        public ActionResult FilterForListUserBelongRole([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Name",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Mail",
                DisplayName = "Email",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Mail") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Role", "Role"));
            var viewModel = new FilterViewModel
            {
                Controller = "Role",
                Action = "ListMembersInRole",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForAddUserToRole([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Name",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Mail",
                DisplayName = "Email",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Mail") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Role", "Role"));
            var viewModel = new FilterViewModel
            {
                Controller = "Role",
                Action = "AddUsersToRole",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }

        public ActionResult FilterForRemoveUsersFromRole([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();

            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Name",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Mail",
                DisplayName = "Email",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Mail") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Role", "Role"));
            var viewModel = new FilterViewModel
            {
                Controller = "Role",
                Action = "RemoveUsersFromRole",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }
    }
}