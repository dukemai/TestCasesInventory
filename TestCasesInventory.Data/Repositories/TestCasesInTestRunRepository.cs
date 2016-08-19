﻿using PagedList;
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

        public TestCasesInTestRunDataModel GetTestCaseInTestRunByID(int testCaseInTestRunID)
        {
            return dataContext.TestCaseInTestRuns.Find(testCaseInTestRunID);
        }
        public void InsertTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            dataContext.TestCaseInTestRuns.Add(testCaseInTestRun);
        }
        public void DeleteTestCaseInTestRun(int testCaseInTestRunID)
        {
            var TestCaseInTestRunDataModel = dataContext.TestCaseInTestRuns.Find(testCaseInTestRunID);
            dataContext.TestCaseInTestRuns.Remove(TestCaseInTestRunDataModel);
        }
        public void UpdateTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            dataContext.Entry(testCaseInTestRun).State = EntityState.Modified;
        }
        public void Save()
        {
            dataContext.SaveChanges();
        }



        public IPagedList<TestCasesInTestRunDataModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            IQueryable<TestCasesInTestRunDataModel> query = dataContext.TestCaseInTestRuns.Where(t => t.TestRunID == testRunId).Select(t => t);

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
                    case "testcase":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.TestCase.Title) : query.OrderByDescending(t => t.Created);
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

            if (filterOptions.PagingOptions != null)
            {
                var pagingOption = filterOptions.PagingOptions;
                return query.ToCustomPagedList(pagingOption.CurrentPage, pagingOption.PageSize);
            }
            return query.ToCustomPagedList(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
        }
    }
}