define(['App/TestRunResult/Models/detaildatamodel', 'App/TestRunResult/testrunresult-routes', 'promise'], function (detailDataModel, routes, promise) {
    function testRunResultModel(id) {
        this.ID = id
        this.data = {};
    }

    testRunResultModel.prototype.getTestRunResultData = function () {
        var self = this;
        return promise.resolve($.get(routes.getTestRunDetailData + self.ID, function (data) {
            self.data = new detailDataModel(data.ID, data.TestRunID, data.Status, data.TestRunTitle, data.NumberOfPassedTestCases, data.NumberOfFailedTestCases, data.NumberOfTestCases, data.NumberOfSkippedTestCases, data.Created, data.LastModified, data.CreatedDate, data.LastModifiedDate);
        }));
    }

    return testRunResultModel;
});