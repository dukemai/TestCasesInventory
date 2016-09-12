define(['promise', 'underscore', 'highCharts'],
    function (promise, _) {
        function statusChartView(data) {
            this.model = data;
        }

        statusChartView.prototype.initChart = function () {
            var self = this;
            var chartArea = $('#status-chart');
            if (chartArea.length) {
                var chart = {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    
                    borderColor: "gray",
                    //#90ed7d
                    borderRadius: 1,
                    borderWidth: 4

                };
                var colors = ['#53a6ff', '#ff5853', '#5be049'];
                var title = {
                    text: 'Test Run Results By Status Overview'
                };
                var subtitle = {
                    text: self.model.TestRunTitle,
                };
                var credits = {
                    href: "#",
                    text: "NYT 2016"
                }
                var tooltip = {
                    //pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    pointFormat: '<b>{point.y}</b>',
                    hideDelay: 300
                };
                var plotOptions = {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name} </b>: {point.percentage:.2f} %',
                            style: {
                                color: 'black'
                                //(Highcharts.theme && Highcharts.theme.contrastTextColor) || 
                            }
                        }
                    }
                };
                var resultInfo =
                     [
                    { "name": "Skipped", "y": self.model.NumberOfSkippedTestCases },
                    { "name": "Failed", "y": self.model.NumberOfFailedTestCases },
                    { "name": "Passed", "y": self.model.NumberOfPassedTestCases },
                     ]
                var series = [{
                    type: 'pie',
                    name: 'Test Cases',
                    data: resultInfo
                }];

                var chartInfo = {};
                chartInfo.chart = chart;
                chartInfo.colors = colors
                chartInfo.title = title;
                chartInfo.subtitle = subtitle;
                chartInfo.credits = credits;
                chartInfo.tooltip = tooltip;
                chartInfo.series = series;
                chartInfo.plotOptions = plotOptions;

                chartArea.highcharts(chartInfo);
            }
        }

        statusChartView.prototype.render = function () {
            var self = this;
            self.initChart();
        }

        return statusChartView;
    });