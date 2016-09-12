define([], function () {
    function testerChartModel(testCasesData) {
        this.testerData = [];
        this.TestCasesData = testCasesData;
    }

    testerChartModel.prototype.statistic = function () {
        var self = this;
        self.testerData = _.map(self.TestCasesData)
    }

    return testerChartModel;
});