define(['App/routes'], function (routes) {
    var testCasesInTestRunRoutes = {
        getUser: '/Tests/TestCasesInTestRun/GetUsersPopUp/',
        assignTestCaseToUser: '/Tests/TestCasesInTestRun/AssignTestCaseToUser/',
        assignTestCaseToMe: '/Tests/TestCasesInTestRun/AssignToMe/'
    };

    _.extend(testCasesInTestRunRoutes, routes);

    return testCasesInTestRunRoutes;
});