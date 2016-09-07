define(['App/TestCasesInTestRun/Models/usermodel', 'App/TestCasesInTestRun/testcasesintestrun-routes', 'promise'], function (userModel, routes, promise) {
    function testCasesInTestRunModel(id) {
        this.ID = id;
        this.Users = [];
    }

    testCasesInTestRunModel.prototype.loadUsers = function () {
        var self = this;
        return promise.resolve($.get(routes.getUser + self.ID, function (data) {
            self.Users = _.map(data, function (userData) {
                return new userModel(userData.ID, userData.Email, userData.DisplayName, userData.TeamID)
            });
        }));
    }
    
    return testCasesInTestRunModel;
});