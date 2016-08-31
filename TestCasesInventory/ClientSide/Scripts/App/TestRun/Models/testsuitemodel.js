define(['App/TestRun/Models/testcasemodel', 'App/TestRun/testrun-routes', 'promise'], function (testCaseModel, routes, promise) {
    function testSuiteModel(id, title) {
        this.ID = id;
        this.Title = title;
        this.TestCases = [];
        this.TestCasesToAdd = [];
        this.TestCasesToRemove = [];
    }

    testSuiteModel.prototype.loadTestCases = function (testRunID) {
        var self = this;
        return promise.resolve($.get(routes.getTestCase + '?testSuiteID=' + self.ID + '&testRunID=' + testRunID, function (data) {
            self.TestCases = _.map(data, function (testCaseData) {
                return new testCaseModel(testCaseData.ID, testCaseData.Title, testCaseData.Checked, testCaseData.Priority, testCaseData.CreatedDate, testCaseData.CreatedDisplayOnly, testCaseData.PriorityStyleClass);
            });
        }));
    }

    testSuiteModel.prototype.addTestCase = function (testCase) {
        var self = this;
        self.TestCasesToAdd.push(testCase);
        self.TestCasesToRemove = _.without(self.TestCasesToRemove, testCase);
    }

    testSuiteModel.prototype.removeTestCase = function (testCase) {
        var self = this;
        self.TestCasesToRemove.push(testCase);
        self.TestCasesToAdd = _.without(self.TestCasesToAdd, testCase);
    }

    return testSuiteModel;
});