define(['App/TestRunResult/Models/testermodel'], function (TesterModel) {
    function testerChartModel(testCasesData) {
        this.testersData = [];
        this.TestCasesData = testCasesData;
    }

    testerChartModel.prototype.statistic = function () {
        var self = this;
        _.each(self.TestCasesData, function (testCase) {
            var tester = _.findWhere(self.testersData, { Name: testCase.RunBy });
            if (tester) {
                tester.TotalTestCases++;
            }
            else
            {
                tester = new TesterModel(testCase.RunBy);
                self.testersData.push(tester);
            }
        });
    }

    return testerChartModel;
});