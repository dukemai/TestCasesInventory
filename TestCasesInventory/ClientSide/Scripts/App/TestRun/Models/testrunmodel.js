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

    return testRunModel;
});