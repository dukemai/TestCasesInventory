using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;

namespace TestCasesInventory.Data.Repositories
{
    public class TestCaseRepository : RepositoryBase, ITestCaseRepository
    {
        public TestCaseRepository() : base() { }

        public IEnumerable<TestCaseDataModel> ListAll(int testSuiteID)
        {
            return dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID).ToList();
        }

        public TestCaseDataModel GetTestCaseByID(int testCaseID)
        {
            return dataContext.TestCases.Find(testCaseID);
        }

        public void InsertTestCase(TestCaseDataModel testCase)
        {
            dataContext.TestCases.Add(testCase);
        }


        public void DeleteTestCase(int testCaseID)
        {
            var TestCaseDataModel = dataContext.TestCases.Find(testCaseID);
            dataContext.TestCases.Remove(TestCaseDataModel);
        }

        public void UpdateTestCase(TestCaseDataModel testCase)
        {
            dataContext.Entry(testCase).State = EntityState.Modified;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TestCaseDataModel> GetTestCasesBeSearchedByName(int testSuiteID, string title)
        {
            var testCasesForTestSuite = dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID);
            return testCasesForTestSuite.Where(t => t.Title.Contains(title)).ToList();
        }

        public int TotalTestCasesForTestSuite(int testSuiteID)
        {
            return dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID).Count();
        }

        public IPagedList<TestCaseDataModel> GetTestCasesForTestSuite(int testSuiteId, FilterOptions filterOptions)
        {
            IQueryable<TestCaseDataModel> query = dataContext.TestCases.Where(t => t.ID == testSuiteId).Select(t => t);
            if (filterOptions == null)
            {
                return query.ToCustomPagedList<TestCaseDataModel>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
            }
            if (!string.IsNullOrEmpty(filterOptions.Keyword))
            {
                foreach (var field in filterOptions.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "title":
                            query = query.Where(t => t.Title.Contains(filterOptions.Keyword));
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
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title);
                        break;
                    default:
                        query = query.OrderBy(t => t.ID);
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
