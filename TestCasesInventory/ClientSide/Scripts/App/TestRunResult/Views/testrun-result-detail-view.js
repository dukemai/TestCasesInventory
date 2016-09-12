define(['App/TestRunResult/Models/testrunresultdetailmodel', 'App/TestRunResult/Views/detail-info-view', 'App/TestRunResult/Views/status-chart-view', 'App/TestRunResult/Views/testcases-info-view', 'promise', 'underscore'],
    function (testRunResultDetailModel, DetailInfoView, ChartStatusView, TestCasesInfo, promise, _) {
        function testRunResultDetailView(id) {
            this.model = new testRunResultDetailModel(id);
            this.detailInfoView = {};
            this.chartStatusView = {};
            this.testCasesInfo = {};
        }

        testRunResultDetailView.prototype.render = function () {
            var self = this;
            self.model.getTestRunResultData().then(
                function () {
                    self.detailInfoView = new DetailInfoView(self.model.data);
                    self.chartStatusView = new ChartStatusView(self.model.data);
                    self.testCasesInfo = new TestCasesInfo(self.model.ID);

                    self.detailInfoView.render();
                    self.chartStatusView.render();
                    self.testCasesInfo.render();

                });
        }

        return testRunResultDetailView;
    });