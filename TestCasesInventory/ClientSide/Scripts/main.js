require(['app'], function (app) {
    app.init();
    app.initAddTestCasesToTestRun();
    app.initDeleteTestCasesInTestRunConfirmation();
});