define(['App/routes'], function (routes) {
    var testRunRoutes = {
        getTestSuite: '/Tests/TestCasesInTestRun/GetTestSuitesPopUp/',
        getTestCase: '/Tests/TestCasesInTestRun/GetTestCasesInTestSuitePopUp/',
        addTestCasesToTestRun: '/Tests/TestCasesInTestRun/AddTestCasesToTestRun',
        removeTestCasesFromTestRun: '/Tests/TestCasesInTestRun/RemoveTestCasesFromTestRun'
    };

    _.extend(testRunRoutes, routes);

    return testRunRoutes;
});

