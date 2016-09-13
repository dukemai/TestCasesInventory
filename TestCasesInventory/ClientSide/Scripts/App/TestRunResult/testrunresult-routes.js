define(['App/routes'], function (routes) {
    var testRunResultRoutes = {
        createTestRunResult: '/Admin/TestRunResulte/Create',
        getTestCasesAssignedToMe: '/Admin/TestRunResult/GetTestCasesAssignedToMe/',
        getTestCasesInTestRunResults: '/Admin/TestRunResult/GetAllTestCases/',
        createTestCaseResult: '/Admin/TestCaseResult/CreateTestCaseResult/',
        finishTestRunResult: '/Admin/TestRunResult/FinishTestRunResult/',
        getTestRunResult: '/Admin/TestRunResult/GetTestRunResult',
        getTestRunDetailData: '/Admin/TestRunResult/TestRunResultData/'
    };

    _.extend(testRunResultRoutes, routes);

    return testRunResultRoutes;
});