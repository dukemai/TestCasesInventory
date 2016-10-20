define(['App/routes'], function (routes) {
    var testRunResultRoutes = {
        createTestRunResult: '/Tests/TestRunResulte/Create',
        getTestCasesAssignedToMe: '/Tests/TestRunResult/GetTestCasesAssignedToMe/',
        getTestCasesInTestRunResults: '/Tests/TestRunResult/GetAllTestCases/',
        createTestCaseResult: '/Tests/TestCaseResult/CreateTestCaseResult/',
        finishTestRunResult: '/Tests/TestRunResult/FinishTestRunResult/',
        getTestRunResult: '/Tests/TestRunResult/GetTestRunResult',
        getTestRunDetailData: '/Tests/TestRunResult/TestRunResultData/'
    };

    _.extend(testRunResultRoutes, routes);

    return testRunResultRoutes;
});