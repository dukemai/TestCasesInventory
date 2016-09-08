define(['templateHelper','promise', 'underscore'],
    function (templateHelper, promise, _) {
        function testerChartView(data) {
            this.model = data;
            this.template = '';
        }

        testerChartView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('tester-chart', '/ClientSide/Templates/TestRunResult/testrun-result-tester-chart.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['tester-chart'];
                    $('#tester-chart').append(self.template(self.model));
                })
            }
        }

        return testerChartView;
    });