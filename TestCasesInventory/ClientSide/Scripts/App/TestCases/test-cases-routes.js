define(['App/routes'], function (routes) {
    var testCasesRoutes = {
        detailTestCases: '/Tests/TestCase/Details'
    };

    _.extend(testCasesRoutes, routes);

    return testCasesRoutes;
});