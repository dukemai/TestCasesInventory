define(['App/TestRunResult/Models/testcasesintestrunresultmodel', 'App/TestRunResult/testrunresult-routes', 'promise'], function (testCasesInTestRunResultModel, routes, promise) {
    function testRunResultModel(id) {
        this.ID = id;
        this.TestCasesInTestRunResults = [];
    }

    testRunResultModel.prototype.loadTestCasesInTestRunResults = function () {
        var self = this;
        return promise.resolve($.get(routes.getTestCasesInTestRunResults + self.ID, function (data) {
            self.TestCasesInTestRunResults = _.map(data, function (testCaseData) {
                return new testCasesInTestRunResultModel(testCaseData.ID, testCaseData.TestCaseTitle, testCaseData.TestCasePriority, testCaseData.TestCaseDescription, testCaseData.TestCasePrecondition, testCaseData.TestCaseExpect, testCaseData.Status, testCaseData.Comment, testCaseData.PriorityStyleClass, testCaseData.StatusStyleClass, testCaseData.RunBy, testCaseData.LastModifiedDate);
            });
        }));
    }

    return testRunResultModel;
});