define(['promise', 'underscore', 'highCharts', 'exporting'],
    function (promise, _) {
        function statusChartView(data) {
            this.model = data;
        }

        statusChartView.prototype.render = function () {
            var self = this;
            $('#status-chart') ;
        }

        return statusChartView;
    });