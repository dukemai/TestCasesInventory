define(['App/routes'], function (routes) {
    var testRunResultRoutes = {
        createTestRunResult: '/Admin/TestRunResulte/Create/',
        getTestCasesInTestRunResults: '/Admin/TestRunResult/GetAllTestCases/',
        getTestRunDetailData: '/Admin/TestRunResult/TestRunResultData/'
    };

    _.extend(testRunResultRoutes, routes);

    return testRunResultRoutes;
});