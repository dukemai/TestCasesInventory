define([], function () {
    function testCasesInTestRunResultModel(id, TestRunID, Status, TestRunTitle, NumberOfPassedTestCases, NumberOfFailedTestCases, NumberOfTestCases, NumberOfSkippedTestCases, Created, LastModified, CreatedDate, LastModifiedDate) {
        this.ID = id;
        this.TestRunID = TestRunID;
        this.Status = Status;
        this.TestRunTitle = TestRunTitle;
        this.NumberOfPassedTestCases = NumberOfPassedTestCases;
        this.NumberOfFailedTestCases = NumberOfFailedTestCases;
        this.NumberOfTestCases = NumberOfTestCases;
        this.NumberOfSkippedTestCases = NumberOfSkippedTestCases;
        this.Created = Created;
        this.LastModified = LastModified;
        this.CreatedDate =  CreatedDate;
        this.LastModifiedDate = LastModifiedDate;
    }
    return testCasesInTestRunResultModel;
});