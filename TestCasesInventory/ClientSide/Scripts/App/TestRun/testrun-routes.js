define(['App/routes'], function (routes) {
    var testRunRoutes = {
        getTestSuite: '/Admin/TestRun/GetTestSuitesPopUp/'
    };

    _.extend(testRunRoutes, routes);

    return testRunRoutes;
});