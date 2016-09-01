define(['App/TestCasesInTestRun/Models/usermodel', 'App/TestCasesInTestRun/testcasesintestrun-routes', 'promise'], function (userModel, routes, promise) {
    function testCasesInTestRunModel(id) {
        this.ID = id;
        this.Users = [];
        this.UserToAssign = [];
    }

    testCasesInTestRunModel.prototype.loadUsers = function () {
        var self = this;
        return promise.resolve($.get(routes.getUser + self.ID, function (data) {
            self.Users = _.map(data, function (userData) {
                return new userModel(userData.ID, userData.Email, userData.DisplayName, userData.TeamID)
            });
        }));
    }

    testCasesInTestRunModel.prototype.getUserToAssign = function () {
        var self = this;
        var listTestCasesToAdd = [];
        for (TestSuite in self.TestSuites) {
            listTestCasesToAdd = _.union(listTestCasesToAdd, self.TestSuites[TestSuite].TestCasesToAdd);
        }
        return listTestCasesToAdd;
    }

    return testCasesInTestRunModel;
});