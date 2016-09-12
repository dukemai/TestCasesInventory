using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;
using TestCasesInventory.Data.Common;

namespace TestCasesInventory.Data.Repositories
{
    public class TestCaseResultRepository : RepositoryBase, ITestCaseResultRepository
    {
        public TestCaseResultRepository() : base() { }

        public IEnumerable<TestCaseResultDataModel> ListAll(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID).ToList();
        }

        public TestCaseResultDataModel GetTestCaseResultByID(int testCaseResultID)
        {
            return dataContext.TestCaseResults.Find(testCaseResultID);
        }

        public TestCaseResultDataModel GetTestCaseResult(int testCaseInTestRunID, int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestCasesInTestRunID == testCaseInTestRunID && t.TestRunResultID == testRunResultID).FirstOrDefault();
        }

        public void InsertTestCaseResult(TestCaseResultDataModel testCaseResult)
        {
            dataContext.TestCaseResults.Add(testCaseResult);
        }

        public void DeleteTestCaseResult(int testCaseResultID)
        {
            var testCaseResultDataModel = dataContext.TestCaseResults.Find(testCaseResultID);
            dataContext.TestCaseResults.Remove(testCaseResultDataModel);
        }

        public void UpdateTestCaseResult(TestCaseResultDataModel testCaseResult)
        {
            dataContext.Entry(testCaseResult).State = EntityState.Modified;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public int TotalTestCaseResultsForTestRunResult(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID).Count();
        }

        public int TotalPassedTestCaseResults(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID && t.Status == TestCaseResultStatus.Passed).Count();
        }

        public int TotalFailedTestCaseResults(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID && t.Status == TestCaseResultStatus.Failed).Count();
        }

        public int TotalSkippedTestCaseResults(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID && t.Status == TestCaseResultStatus.Skipped).Count();
        }

        public IPagedList<TestCaseResultDataModel> GetTestCasesForTestSuite(int testRunResultId, FilterOptions options)
        {
            IQueryable<TestCaseResultDataModel> query = dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultId).Select(t => t);
            if (options == null)
            {
                return query.ToCustomPagedList<TestCaseResultDataModel>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
            }
            if (!string.IsNullOrEmpty(options.Keyword))
            {
                foreach (var field in options.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "status":
                            query = query.Where(t => t.Status.Contains(options.Keyword));
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
                    case "status":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Status) : query.OrderByDescending(t => t.Status);
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

       
    }
    
}
