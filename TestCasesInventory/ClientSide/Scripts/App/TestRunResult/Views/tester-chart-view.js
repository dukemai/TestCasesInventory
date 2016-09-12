﻿define(['App/TestRunResult/Models/testerchartmodel', 'promise', 'underscore', 'highCharts'],
    function (testerChartModel,promise, _) {
        function testerChartView(TestCasesData) {
            this.model = new testerChartModel(TestCasesData);
        }
        testerChartView.prototype.initChart = function () {
            var self = this;
            var chartArea = $('#tester-chart');
            if (chartArea.length) {
                var chart = {
                    backgroundColor: "#f3f3f3",
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    borderColor: "gray",
                    borderRadius: 1,
                    borderWidth: 4

                };
                var title = {
                    text: 'Test Run Results By Tester Overview'
                };
                var subtitle = {
                    text: self.model.TestRunTitle 
                };
                var credits = {
                    href: "#",
                    text: "NYT 2016"
                }
                var tooltip = {
                    //pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    pointFormat: ' Number of {series.name}: <b>{point.y}</b>',
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
                    { "name": "User1", "y": 9 },
                    { "name": "User2", "y": 7 },
                    { "name": "Admin", "y": 25 }
                     ]
                var series = [{
                    type: 'pie',
                    name: 'Test Cases',
                    data: resultInfo
                }];

                var chartInfo = {};
                chartInfo.chart = chart;
                chartInfo.title = title;
                chartInfo.subtitle = subtitle;
                chartInfo.credits = credits;
                chartInfo.tooltip = tooltip;
                chartInfo.series = series;
                chartInfo.plotOptions = plotOptions;

                chartArea.highcharts(chartInfo);
            }
        }
        testerChartView.prototype.render = function () {
            var self = this;
            self.model.statistic();
            console.log(self.model.testersData);
        }

        return testerChartView;
    });