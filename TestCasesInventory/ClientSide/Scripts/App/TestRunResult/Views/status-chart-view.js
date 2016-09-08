define(['templateHelper','promise', 'underscore'],
    function (templateHelper, promise, _) {
        function statusChartView(data) {
            this.model = data;
            this.template = '';
        }

        statusChartView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('status-chart', '/ClientSide/Templates/TestRunResult/tesrun-result-status-chart.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['status-chart'];
                    $('#status-chart').append(self.template(self.model));
                })
            }
        }

        return statusChartView;
    });