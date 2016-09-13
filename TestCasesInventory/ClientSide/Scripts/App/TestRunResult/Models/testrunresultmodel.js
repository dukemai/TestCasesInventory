define(['App/TestRunResult/Models/testcasesintestrunresultmodel', 'App/TestRunResult/testrunresult-routes', 'promise', 'App/TestRunResult/Common/options-run-common'], function (testCasesInTestRunResultModel, routes, promise, common) {
    function testRunResultModel(id) {
        this.ID = id;
        this.TestCasesInTestRunResults = [];
    }

    testRunResultModel.prototype.loadTestCasesInTestRunResults = function (selected) {
        var self = this;
        var option = new common();
        if (selected == option.getAll)
        {
            return promise.resolve($.get(routes.getTestCasesInTestRunResults + self.ID, function (data) {
                self.TestCasesInTestRunResults = _.map(data, function (testCaseData) {
                    return new testCasesInTestRunResultModel(testCaseData.ID, testCaseData.TestCaseTitle, testCaseData.TestCasePriority, testCaseData.TestCaseDescription, testCaseData.TestCasePrecondition, testCaseData.TestCaseExpect, testCaseData.Status, testCaseData.Comment, testCaseData.PriorityStyleClass, testCaseData.StatusStyleClass, testCaseData.RunBy, testCaseData.LastModifiedDate, testCaseData.TestRunTitle);
                });
            }));
        }
        else
        {
            return promise.resolve($.get(routes.GetTestCasesAssignedToMe + self.ID, function (data) {
                self.TestCasesInTestRunResults = _.map(data, function (testCaseData) {
                    return new testCasesInTestRunResultModel(testCaseData.ID, testCaseData.TestCaseTitle, testCaseData.TestCasePriority, testCaseData.TestCaseDescription, testCaseData.TestCasePrecondition, testCaseData.TestCaseExpect, testCaseData.Status, testCaseData.Comment, testCaseData.PriorityStyleClass, testCaseData.StatusStyleClass, testCaseData.RunBy, testCaseData.LastModifiedDate, testCaseData.TestRunTitle);
                });
            }));
        }
        
    }

    return testRunResultModel;
});