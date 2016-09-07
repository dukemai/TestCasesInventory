require(['app'], function (app) {
    app.init();
    app.initAddTestCasesToTestRun();
    app.initDeleteTestCasesInTestRunConfirmation();
    app.initAssignTestCaseToUser();
    app.initRunTestRun();
    app.initShowChart();
});