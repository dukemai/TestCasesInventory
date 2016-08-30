define(['App/routes'], function (routes) {
    var testRunRoutes = {
        getTestSuite: '/Admin/TestRun/GetTestSuitesPopUp/',
        getTestCase: '/Admin/TestRun/GetTestCasesInTestSuitePopUp/',
        postListToRemove: '/Admin/',
        postListToAdd: '/Admin/'
    };

    _.extend(testRunRoutes, routes);

    return testRunRoutes;
});