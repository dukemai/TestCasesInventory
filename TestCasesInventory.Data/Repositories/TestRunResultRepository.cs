using System;
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
    public class TestRunResultRepository : RepositoryBase, ITestRunResultRepository
    {
        public TestRunResultRepository() : base() {}

        public IEnumerable<TestRunResultDataModel> ListAll(FilterOptions options, int testRunID)
        {
            IQueryable<TestRunResultDataModel> query = dataContext.TestRunResults.Where(t => t.TestRunID == testRunID).Select(t => t);

            if (options.SortOptions != null)
            {
                var sortOptions = options.SortOptions;
                switch (sortOptions.Field.ToLowerInvariant())
                {
                    case "status":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Status) : query.OrderByDescending(t => t.Status);
                        break;
                    case "id":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.ID) : query.OrderByDescending(t => t.ID);
                        break;
                    case "created":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.CreatedDate) : query.OrderByDescending(t => t.CreatedDate);
                        break;
                    case "lastmodified":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.LastModifiedDate) : query.OrderByDescending(t => t.LastModifiedDate);
                        break;
                    case "createdby":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Created) : query.OrderByDescending(t => t.Created);
                        break;
                    default:
                        query = query.OrderByDescending(d => d.CreatedDate);
                        break;
                }
                query = OrderByID(query);
            }

            return query.ToList();
        }
        
        

        public TestRunResultDataModel GetTestRunResultByID(int testRunResultID)
        {
            return dataContext.TestRunResults.Find(testRunResultID);
        }

        public void InsertTestRunResult(TestRunResultDataModel testRunResult)
        {
            dataContext.TestRunResults.Add(testRunResult);
        }

        public void UpdateTestRunResult(TestRunResultDataModel testRunResult)
        {
            dataContext.Entry(testRunResult).State = EntityState.Modified;
        }

        public void DeleteTestRunResult(int testRunResultID)
        {
            TestRunResultDataModel testRunResultDataModel = dataContext.TestRunResults.Find(testRunResultID);
            dataContext.TestRunResults.Remove(testRunResultDataModel);
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TestCaseResultDataModel> ListTestCaseResultsForTestRunResult(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID).ToList();
        }

        //public IPagedList<TestRunResultDataModel> GetTestRunResults(FilterOptions options, int testRunID)
        //{
        //    IQueryable<TestRunResultDataModel> query = dataContext.TestRunResults.Where(t => t.TestRunID == testRunID).Select(t => t);

        //    if (options == null)
        //    {
        //        return query.ToCustomPagedList<TestRunResultDataModel>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
        //    }
        //    if (!string.IsNullOrEmpty(options.Keyword))
        //    {
        //        foreach (var field in options.FilterFields)
        //        {
        //            switch (field.ToLowerInvariant())
        //            {
        //                case "status":
        //                    query = query.Where(t => t.Status.Contains(options.Keyword));
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }

        //    if (options.SortOptions != null)
        //    {
        //        var sortOptions = options.SortOptions;
        //        switch (sortOptions.Field.ToLowerInvariant())
        //        {
        //            case "status":
        //                query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Status) : query.OrderByDescending(t => t.Status);
        //                break;
        //            case "testrunresultid":
        //                query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.ID) : query.OrderByDescending(t => t.ID);
        //                break;
        //            case "started":
        //                query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.CreatedDate) : query.OrderByDescending(t => t.CreatedDate);
        //                break;
        //            case "finished":
        //                query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.LastModifiedDate) : query.OrderByDescending(t => t.LastModifiedDate);
        //                break;
        //            case "startedby":
        //                query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Created) : query.OrderByDescending(t => t.Created);
        //                break;
        //            default:
        //                query = query.OrderByDescending(d => d.CreatedDate);
        //                break;
        //        }
        //        query = OrderByID(query);
        //    }

        //    if (options.PagingOptions != null)
        //    {
        //        var pagingOption = options.PagingOptions;
        //        return query.ToCustomPagedList(pagingOption.CurrentPage, pagingOption.PageSize);
        //    }
        //    return query.ToCustomPagedList(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
        //}

        public TestRunResultDataModel GetTestRunResultsInProgress(int testRunID)
        {
            return dataContext.TestRunResults.Where(t => t.Status == "In Progress" && t.TestRunID == testRunID).FirstOrDefault();
        }
    }
}
