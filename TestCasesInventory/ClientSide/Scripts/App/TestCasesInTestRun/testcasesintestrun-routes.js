define(['App/routes'], function (routes) {
    var testCasesInTestRunRoutes = {
        getUser: '/Admin/TestCasesInTestRun/GetUsersPopUp/',
        assignTestCaseToUser: '/Admin/TestCasesInTestRun/AssignTestCaseToUser/'
    };

    _.extend(testCasesInTestRunRoutes, routes);

    return testCasesInTestRunRoutes;
});