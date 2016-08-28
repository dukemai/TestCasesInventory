define(['App/TestRun/Models/testcasemodel'], function (testCaseModel) {
    function testSuiteModel(id, title) {
        this.ID = id;
        this.Title = title;
        this.TestCases = [];
    }

    return testSuiteModel;
});