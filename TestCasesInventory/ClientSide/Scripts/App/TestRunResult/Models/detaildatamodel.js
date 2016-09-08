define([], function () {
    function testCasesInTestRunResultModel(id, TestRunID, Status, TestRunTitle, NumberOfPassedTestCases, NumberOfFailedTestCases, NumberOfTestCases, NumberOfSkippedTestCases) {
        this.ID = id;
        this.TestRunID = TestRunID;
        this.Status = Status;
        this.TestRunTitle = TestRunTitle;
        this.NumberOfPassedTestCases = NumberOfPassedTestCases;
        this.NumberOfFailedTestCases = NumberOfFailedTestCases;
        this.NumberOfTestCases = NumberOfTestCases;
        this.NumberOfSkippedTestCases = NumberOfSkippedTestCases;
    }
    return testCasesInTestRunResultModel;
});