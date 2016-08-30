define(['App/routes'], function (routes) {
    var testRunRoutes = {
        getTestSuite: '/Admin/TestRun/GetTestSuitesPopUp/',
        getTestCase: '/Admin/TestRun/GetTestCasesInTestSuitePopUp/',
        addTestCasesToTestRun: '/Admin/TestCasesInTestRun/AddTestCasesToTestRun',
        removeTestCasesFromTestRun: '/Admin/TestCasesInTestRun/RemoeTestCasesFromTestRun'
    };

    _.extend(testRunRoutes, routes);

    return testRunRoutes;
});

