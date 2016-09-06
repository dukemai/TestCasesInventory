define(['App/routes'], function (routes) {
    var testCasesInTestRunRoutes = {
        getUser: '/Admin/TestCasesInTestRun/GetUsersPopUp/',
        assignTestCaseToUser: '/Admin/TestCasesInTestRun/AssignTestCaseToUser/',
        assignTestCaseToMe: '/Admin/TestCasesInTestRun/AssignToMe/'
    };

    _.extend(testCasesInTestRunRoutes, routes);

    return testCasesInTestRunRoutes;
});