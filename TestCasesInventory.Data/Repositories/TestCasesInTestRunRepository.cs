using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TestCasesInTestRunRepository : RepositoryBase, ITestCasesInTestRunRepository
    {
        public TestCasesInTestRunRepository() : base() { }

        public TestCasesInTestRunDataModel GetTestCaseInTestRunByID(int testCaseInTestRunID)
        {
            return dataContext.TestCasesInTestRuns.Find(testCaseInTestRunID);
        }

        public IEnumerable<TestCasesInTestRunDataModel> GetTestCasesInTestRun(int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID).ToList();
        }

        public void AddTestCasesToTestRun(List<TestCasesInTestRunDataModel> testCasesInTestRunData)
        {
            foreach (var testCaseInTestRun in testCasesInTestRunData)
            {
                if (GetTestCaseInTestRun(testCaseInTestRun.TestCaseID, testCaseInTestRun.TestRunID) == null)
                {
                    dataContext.TestCasesInTestRuns.Add(testCaseInTestRun);
                }
            }
        }

        public TestCasesInTestRunDataModel GetTestCaseInTestRun(int testCaseID, int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID && t.TestCaseID == testCaseID).FirstOrDefault();
        }

        public void RemoveTestCasesFromTestRun(List<int> testCasesIDs, int testRunID)
        {
            foreach (var testCaseID in testCasesIDs)
            {
                var testCaseInTestRun = GetTestCaseInTestRun(testCaseID, testRunID);
                if (testCaseInTestRun != null)
                {
                    dataContext.TestCasesInTestRuns.Remove(testCaseInTestRun);
                }
            }
        }

        public void AssignTestCaseToUser(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            dataContext.Entry(testCaseInTestRun).State = EntityState.Modified;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public int TotalTestCasesInTestRun(int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID).Count();
        }

        public IPagedList<TestCasesInTestRunDataModel> GetPagedListTestCasesByTestRun(int testRunId, FilterOptions filterOptions)
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
                    case "title":
                        query = sortOptions.Direction == SortDirections.Desc ? query.OrderByDescending(t => t.TestCase.Title) : query.OrderBy(t => t.TestCase.Title);
                        break;
                    case "priority":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.TestCase.Priority) : query.OrderByDescending(t => t.TestCase.Priority);
                        break;
                    case "assignedby":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.AssignedBy) : query.OrderByDescending(t => t.AssignedBy);
                        break;
                    case "assignedto":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.AssignedTo) : query.OrderByDescending(t => t.AssignedTo);
                        break;
                    default:
                        query = query.OrderBy(d => d.TestCase.Title);
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
