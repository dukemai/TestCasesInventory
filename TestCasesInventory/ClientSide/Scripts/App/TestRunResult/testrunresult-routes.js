define(['App/routes'], function (routes) {
    var testRunResultRoutes = {
        createTestRunResult: '/Admin/TestRunResulte/Create',
        getTestCasesInTestRunResults: '/Admin/TestRunResult/GetAllTestCases/',
        createTestCaseResult: '/Admin/TestCaseResult/CreateTestCaseResult/',
        finishTestRunResult: '/Admin/TestRunResult/FinishTestRunResult/',
        getTestRunResult: '/Admin/TestRunResult/GetTestRunResult',
        getTestRunDetailData: '/Admin/TestRunResult/TestRunResultData/'
    };

    _.extend(testRunResultRoutes, routes);

    return testRunResultRoutes;
});