define(['App/routes'], function (routes) {
    var testRunResultRoutes = {
        createTestRunResult: '/Admin/TestRunResulte/Create/',
        getTestCasesInTestRunResults: '/Admin/TestRunResult/GetAllTestCases/'
    };

    _.extend(testRunResultRoutes, routes);

    return testRunResultRoutes;
});