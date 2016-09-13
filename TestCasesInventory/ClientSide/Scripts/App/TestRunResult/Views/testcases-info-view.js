define(['App/TestRunResult/Models/testrunresultmodel', 'App/TestRunResult/Views/tester-chart-view', 'App/TestRunResult/Views/testcasesresult-table-view', 'App/TestRunResult/Common/options-run-common'], function (testRunResultModel, TesterChartView, TestCasesResultTable, common) {
    function testCasesInfoView(id) {
        this.model = new testRunResultModel(id);
        this.testerChartView = {};
        this.testCasesResultTable = {};
    }

    testCasesInfoView.prototype.render = function () {
        var self = this;
        var option = new common();
        self.model.loadTestCasesInTestRunResults(option.getAll).then(function () {
            self.testerChartView = new TesterChartView(self.model.TestCasesInTestRunResults);
            self.testCasesResultTable = new TestCasesResultTable(self.model.TestCasesInTestRunResults);

            self.testCasesResultTable.render();
            self.testerChartView.render();
        });
    }

    return testCasesInfoView;
});