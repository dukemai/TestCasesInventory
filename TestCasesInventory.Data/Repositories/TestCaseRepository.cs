﻿using System;
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

        public int TotalTestCasesForTestSuite(int testSuiteID)
        {
            return dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID).Count();
        }

        public IPagedList<TestCaseDataModel> GetTestCasesForTestSuite(int testSuiteId, FilterOptions filterOptions)
        {
            IQueryable<TestCaseDataModel> query = dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteId).Select(t => t);
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
                    case "priority":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.PriorityValue) : query.OrderByDescending(t => t.PriorityValue);
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
                query = OrderByID(query);
            }

            if (filterOptions.PagingOptions != null)
            {
                var pagingOption = filterOptions.PagingOptions;
                return query.ToCustomPagedList(pagingOption.CurrentPage, pagingOption.PageSize);
            }
            return query.ToCustomPagedList(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
        }

        public static int ConvertPriorityToNumber(string Priority)
        {
            switch (Priority)
            {
                case "Lowest":
                    return 1;
                case "Low":
                    return 2;
                case "Medium":
                    return 3;
                case "High":
                    return 4;
                case "Highest":
                    return 5;
                default:
                    return 3;
            }
        }
    }
}
