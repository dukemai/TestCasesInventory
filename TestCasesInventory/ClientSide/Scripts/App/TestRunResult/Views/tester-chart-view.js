define(['promise', 'underscore', 'highCharts', 'exporting'],
    function (promise, _) {
        function testerChartView(data) {
            this.model = data;
        }

        testerChartView.prototype.render = function () {
            var self = this;
            $('#tester-chart');
        }

        return testerChartView;
    });