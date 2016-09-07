define(['App/routes'], function (routes) {
    var testRunRoutes = {
        getTestSuite: '/Admin/TestCasesInTestRun/GetTestSuitesPopUp/',
        getTestCase: '/Admin/TestCasesInTestRun/GetTestCasesInTestSuitePopUp/',
        addTestCasesToTestRun: '/Admin/TestCasesInTestRun/AddTestCasesToTestRun',
        removeTestCasesFromTestRun: '/Admin/TestCasesInTestRun/RemoveTestCasesFromTestRun'
    };

    _.extend(testRunRoutes, routes);

    return testRunRoutes;
});

