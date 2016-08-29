define(['App/TestRun/Models/testcasemodel', 'App/TestRun/testrun-routes', 'promise'], function (testCaseModel, routes, promise) {
    function testSuiteModel(id, title) {
        this.ID = id;
        this.Title = title;
        this.TestCases = [];
    }

    testSuiteModel.prototype.loadTestCases = function (testRunID) {
        var self = this;
        return promise.resolve($.get(routes.getTestCase + '?testSuiteID=' + self.ID + '&testRunID=' + testRunID, function (data) {
            self.TestCases = _.map(data, function (testCaseData) {
                return new testCaseModel(testCaseData.ID, testCaseData.Title, testCaseData.IsInTestRun)
            });
        }));
    }
    return testSuiteModel;
});