define(['App/TestRunResult/Models/testrunresultmodel', 'App/TestRunResult/Views/tester-chart-view', 'App/TestRunResult/Views/testcasesresult-table-view'], function (testRunResultModel, TesterChartView, TestCasesResultTable) {
    function testCasesInfoView(id) {
        this.model = new testRunResultModel(id);
        this.testerChartView = {};
        this.testCasesResultTable = {};
    }

    testCasesInfoView.prototype.render = function () {
        var self = this;
        self.model.loadTestCasesInTestRunResults().then(function () {
            self.testerChartView = new TesterChartView(self.model.TestCasesInTestRunResults);
            self.testCasesResultTable = new TestCasesResultTable(self.model.TestCasesInTestRunResults);

            self.testCasesResultTable.render();
        });
    }

    return testCasesInfoView;
});