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

        public IPagedList<TestSuiteDataModel> GetTestSuites(FilterOptions options, int? teamID, bool getAll)
        {

            IQueryable<TestSuiteDataModel> query = dataContext.TestSuites.Select(t => t);
            if (!getAll)
            {
                if (teamID.HasValue)
                    query = query.Where(t => t.TeamID == teamID.Value);
                else
                    query = query.Where(t => t.TeamID < 0);
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
                    case "team":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Team.Name) : query.OrderByDescending(t => t.Team.Name);
                        break;
                    case "numbertestcases":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.TestCases.Count) : query.OrderByDescending(t => t.TestCases.Count);
                        break;
                    case "createdby":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Created) : query.OrderByDescending(t => t.Created);
                        break;
                    case "lastmodifiedby":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.LastModified) : query.OrderByDescending(t => t.LastModified);
                        break;
                    case "created":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.CreatedDate) : query.OrderByDescending(t => t.CreatedDate);
                        break;
                    case "lastmodified":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.LastModifiedDate) : query.OrderByDescending(t => t.LastModifiedDate);
                        break;
                    default:
                        query = query.OrderByDescending(d => d.CreatedDate);
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

        public IList<TestSuiteDataModel> GetTestSuitesForTeam(int teamID)
        {
            return dataContext.TestSuites.Where(t => t.TeamID == teamID).ToList();
        }

        public IList<TestSuiteDataModel> GetTestSuitesForUser(string userID)
        {
            return dataContext.TestSuites.Where(t => t.Created == userID || t.LastModified == userID).ToList();
        }

        public List<TestSuiteDataModel> GetTestSuitesPopUp(int? teamID, bool getAll)
        {
            IQueryable<TestSuiteDataModel> query = dataContext.TestSuites.Select(t => t);
            if (!getAll)
            {
                if (teamID.HasValue)
                    query = query.Where(t => t.TeamID == teamID.Value);
                else
                    query = query.Where(t => t.TeamID < 0);
            }
            return query.ToList();
        }
    }
}
