using AutoMapper;
using System;
using System.Linq.Expressions;
using System.Linq;


namespace TestCasesInventory.Presenter.Common
{
    public class ObjectStatus
    {
        public const string InProgress = "In Progress";
        public const string Finished = "Finished";
        public const string Passed = "Passed";
        public const string Failed = "Failed";
        public const string Skipped = "Skipped";
    }
}
