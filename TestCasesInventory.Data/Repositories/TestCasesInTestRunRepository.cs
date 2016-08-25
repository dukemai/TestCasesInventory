using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TestCasesInTestRunRepository: RepositoryBase, ITestCasesInTestRunRepository
    {
        public TestCasesInTestRunRepository() : base() { }

        public IEnumerable<TestCasesInTestRunDataModel> ListAll(int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID).ToList();
        }

        public TestCasesInTestRunDataModel GetTestCaseInTestRunByID(int testCaseInTestRunID)
        {
            return dataContext.TestCasesInTestRuns.Find(testCaseInTestRunID);
        }
        public void InsertTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            dataContext.TestCasesInTestRuns.Add(testCaseInTestRun);
        }
        public void DeleteTestCaseInTestRun(int testCaseInTestRunID)
        {
            var TestCaseInTestRunDataModel = dataContext.TestCasesInTestRuns.Find(testCaseInTestRunID);
            dataContext.TestCasesInTestRuns.Remove(TestCaseInTestRunDataModel);
        }
        public void UpdateTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            dataContext.Entry(testCaseInTestRun).State = EntityState.Modified;
        }
        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TestCasesInTestRunDataModel> TestCaseAlreadyInTestRun(int testRunID, int testCaseID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID && t.TestCaseID == testCaseID).ToList();
        }

        public int TotalTestCasesInTestRun(int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID).Count();
        }

        public IPagedList<TestCasesInTestRunDataModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            IQueryable<TestCasesInTestRunDataModel> query = dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunId).Select(t => t);

            if (filterOptions == null)
            {
                return query.ToCustomPagedList<TestCasesInTestRunDataModel>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
            }

            if (!string.IsNullOrEmpty(filterOptions.Keyword))
            {
                foreach (var field in filterOptions.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "createdby":
                            query = query.Where(t => t.Created.Contains(filterOptions.Keyword));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (filterOptions.SortOptions != null)
            {
                var sortOptions = filterOptions.SortOptions;
                switch (sortOptions.Field.ToLowerInvariant())
                {
                    //case "testcase":
                    //    query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.TestCase.Title) : query.OrderByDescending(t => t.TestCase.Title);
                    //    break;
                    //case "assignedby":
                    //    query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.AssignedBy) : query.OrderByDescending(t => t.AssignedBy);
                    //    break;
                    //case "assignedto":
                    //    query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.AssignedTo) : query.OrderByDescending(t => t.AssignedTo);
                    //    break;
                    default:
                        query = query.OrderByDescending(d => d.TestCase.Title);
                        break;
                }
            }

            if (filterOptions.PagingOptions != null)
            {
                var pagingOption = filterOptions.PagingOptions;
                return query.ToCustomPagedList(pagingOption.CurrentPage, pagingOption.PageSize);
            }
            return query.ToCustomPagedList(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
        }
    }
}
