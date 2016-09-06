define(['App/TestRun/Models/testsuitemodel', 'App/TestRun/testrun-routes', 'promise'], function (testSuiteModel, routes, promise) {
    function testRunModel(id) {
        this.ID = id;
        this.TestSuites = [];
    }
    testRunModel.prototype.loadTestSuites = function () {
        var self = this;
        return promise.resolve($.get(routes.getTestSuite + self.ID, function (data) {
            self.TestSuites = _.map(data, function (testSuiteData) {
                return new testSuiteModel(testSuiteData.ID, testSuiteData.Title)
            });
        }));
    }
    testRunModel.prototype.getTestCasesToAdd = function () {
        var self = this;
        var listTestCasesToAdd = [];
        for (TestSuite in self.TestSuites) {
            listTestCasesToAdd = _.union(listTestCasesToAdd, self.TestSuites[TestSuite].TestCasesToAdd);
        }
        return listTestCasesToAdd;
    }

    testRunModel.prototype.getTestCasesToRemove = function () {
        var self = this;
        var listTestCasesToRemove = [];
        for (TestSuite in self.TestSuites) {
            listTestCasesToRemove = _.union(listTestCasesToRemove, self.TestSuites[TestSuite].TestCasesToRemove);
        }
        return listTestCasesToRemove;
    }

    return testRunModel;
});