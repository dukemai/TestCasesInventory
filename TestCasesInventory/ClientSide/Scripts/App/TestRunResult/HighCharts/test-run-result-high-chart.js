
define(['highCharts', 'exporting'], function () {

    var exportModule = {};

    exportModule.init = function (el) {
        $(document).ready(function () {
            var chartArea = $('#test-run-result-high-chart-container');
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
                var series = [{
                    type: 'pie',
                    name: 'Test Cases',
                    data: [
                       ['Skipped', 9],
                       ['Failed', 7],
                       ['Failed', 25]
                    ]
                       //{
                       //    name: 'Passed Test Cases',
                       //    y: 25,
                       //    sliced: true,
                       //    selected: true
                       //},
                    
                }];

                var json = {};
                json.chart = chart;
                json.title = title;
                json.subtitle = subtitle;
                json.credits = credits;
                json.tooltip = tooltip;
                json.series = series;
                json.plotOptions = plotOptions;
                chartArea.highcharts(json);
            }

        });
    };
    return exportModule;
});