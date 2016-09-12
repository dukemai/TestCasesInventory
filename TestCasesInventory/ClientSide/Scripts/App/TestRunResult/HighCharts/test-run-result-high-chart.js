﻿define(['highCharts', 'exporting'], function () {

    var exportModule = {};

    exportModule.init = function (el) {
        $(document).ready(function () {
            var chartArea = $('#test-run-result-high-chart-container');
            if (chartArea.length) {
                var chart = {
                    backgroundColor: "#e3e99e",
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    borderColor: "gray",
                    //#90ed7d
                    borderRadius: 1,
                    borderWidth: 4

                };
                var title = {
                    text: 'Test Run Results By Status Overview'
                };
                var subtitle = {
                    text: 'Test Run number 2 [hard code]'
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
                    { "name": "Skipped", "y": 9 },
                    { "name": "Failed", "y": 7 },
                    { "name": "Passed", "y": 25 }
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

        });
    };
    return exportModule;
});