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
    public class TestSuiteRepository : RepositoryBase, ITestSuiteRepository, IDisposable
    {
        public TestSuiteRepository()
            : base()
        {
        }

        public IEnumerable<TestSuiteDataModel> ListAll()
        {
            return dataContext.TestSuites.ToList();
        }

        public TestSuiteDataModel GetTestSuiteByID(int testSuiteID)
        {
            return dataContext.TestSuites.Find(testSuiteID);
        }

        public void InsertTestSuite(TestSuiteDataModel testSuite)
        {
            dataContext.TestSuites.Add(testSuite);
        }

        public void UpdateTestSuite(TestSuiteDataModel testSuite)
        {
            dataContext.Entry(testSuite).State = EntityState.Modified;
        }

        public void DeleteTestSuite(int testSuiteID)
        {
            TestSuiteDataModel testSuiteDataModel = dataContext.TestSuites.Find(testSuiteID);
            dataContext.TestSuites.Remove(testSuiteDataModel);
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IList<TestSuiteDataModel> GetTestSuitesBeSearchedByTitle(string title)
        {
            return dataContext.TestSuites.Where(t => t.Title.Contains(title)).ToList();
        }
        public IList<TestSuiteDataModel> GetTestSuitesBeSearchedByTeam(int teamID)
        {
            return dataContext.TestSuites.Where(t => t.TeamID == teamID).ToList();
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

        public IPagedList<TestSuiteDataModel> GetTestSuites(FilterOptions options,string[] roles, int? teamID)
        {
           
            IQueryable<TestSuiteDataModel> query = dataContext.TestSuites.Select(t => t);
            if (!teamID.HasValue && !roles.Contains("Admin"))
            {
                query = query.Where(t => t.TeamID < 0);
            }
            if (teamID.HasValue && !roles.Contains("Admin"))
            {
                query = query.Where(t => t.TeamID == teamID.Value);
            }
            if (options == null)
            {
                return query.ToCustomPagedList<TestSuiteDataModel>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
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
                    default:
                        query = query.OrderBy(t => t.ID);
                        break;
                }
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
