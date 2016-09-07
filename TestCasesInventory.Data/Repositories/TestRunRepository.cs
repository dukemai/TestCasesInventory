using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TestRunRepository : RepositoryBase, ITestRunRepository, IDisposable
    {
        public TestRunRepository()
            : base()
        {
        }

        public IEnumerable<TestRunDataModel> ListAll()
        {
            return dataContext.TestRuns.ToList();
        }

        public TestRunDataModel GetTestRunByID(int testRunID)
        {
            return dataContext.TestRuns.Find(testRunID);
        }

        public void InsertTestRun(TestRunDataModel testRun)
        {
            dataContext.TestRuns.Add(testRun);
        }

        public void UpdateTestRun(TestRunDataModel testRun)
        {
            dataContext.Entry(testRun).State = EntityState.Modified;
        }

        public void DeleteTestRun(int testRunID)
        {
            TestRunDataModel testRunDataModel = dataContext.TestRuns.Find(testRunID);
            dataContext.TestRuns.Remove(testRunDataModel);
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TestCasesInTestRunDataModel> ListTestCasesInTestRun(int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID).ToList();
        }

        public IEnumerable<TestCaseDataModel> ListTestCasesForTestSuite(int testSuiteID)
        {
            return dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID).ToList();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {


            if (!this.disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IPagedList<TestRunDataModel> GetTestRuns(FilterOptions options, int? teamID, bool getAll)
        {

            IQueryable<TestRunDataModel> query = dataContext.TestRuns.Select(t => t);
            if (!getAll)
            {
                if (teamID.HasValue)
                    query = query.Where(t => t.TeamID == teamID.Value);
                else
                    query = query.Where(t => t.TeamID < 0);
            }

            if (options == null)
            {
                return query.ToCustomPagedList<TestRunDataModel>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
            }
            if (!string.IsNullOrEmpty(options.Keyword))
            {
                foreach (var field in options.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "title":
                            query = query.Where(t => t.Title.Contains(options.Keyword));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (options.SortOptions != null)
            {
                var sortOptions = options.SortOptions;
                switch (sortOptions.Field.ToLowerInvariant())
                {
                    case "title":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title);
                        break;
                    case "":
                    default:
                        query = query.OrderByDescending(d => d.CreatedDate);
                        break;
                }
                query = OrderByID(query);
            }

            if (options.PagingOptions != null)
            {
                var pagingOption = options.PagingOptions;
                return query.ToCustomPagedList(pagingOption.CurrentPage, pagingOption.PageSize);
            }
            return query.ToCustomPagedList(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
        }
    }
}
