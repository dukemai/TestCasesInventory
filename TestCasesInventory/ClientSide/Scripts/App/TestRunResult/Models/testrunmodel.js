define(['App/TestRunResult/testrunresult-routes', 'promise', 'App/TestRunResult/Models/testrunresultmodel'], function (routes, promise, testRunResultModel) {
    function testRunModel(id) {
        this.ID = id;
        this.TestRunResult = {};
    }

    testRunModel.prototype.getTestRunResult = function () {
        var self = this;
        return promise.resolve($.get(routes.getTestRunResult + "?testRunID=" + self.ID, function (data) {
            self.TestRunResult = new testRunResultModel(data.testRunResultID);
        }));
    }

    return testRunModel;
});