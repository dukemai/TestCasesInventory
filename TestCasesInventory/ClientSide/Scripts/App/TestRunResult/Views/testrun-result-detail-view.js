define(['App/TestRunResult/Models/testrunresultdetailmodel', 'App/TestRunResult/Views/detail-info-view', 'App/TestRunResult/Views/status-chart-view', 'App/TestRunResult/Views/tester-chart-view', 'promise', 'underscore'],
    function (testRunResultDetailModel, DetailInfoView, ChartStatusView, ChartTesterView, promise, _) {
        function testRunResultDetailView(id) {
            this.model = new testRunResultDetailModel(id);
        }

        testRunResultDetailView.prototype.render = function () {
            var self = this;
            self.model.getTestRunResultData().then(
                function () {
                    var detailInfoView = new DetailInfoView(self.model.data);
                    var chartStatusView = new ChartStatusView(self.model.data);
                    var chartTesterView = new ChartTesterView(self.model.data);

                    detailInfoView.render();
                    chartStatusView.render();
                    chartTesterView.render();

                });
        }

        return testRunResultDetailView;
    });