/// <reference path="../Lib/highcharts.js" />
/// <reference path="../Lib/highcharts.js" />
/// <reference path="../Lib/highcharts.js" />
requirejs.config({
    'baseUrl': '/ClientSide/Scripts',
    'paths': {
        'tinyMCE': 'Lib/tinymce/tinymce.min',
        'tinyMCEInit': 'App/tinymce-init',
        'tabCommonFunctions': 'App/tab-common-functions',
        'fileDeleteConfirmation': 'App/file-delete-confirmation',
        'deleteTestCasesInTestRunConfirmation': 'App/TestRun/delete-testcase-in-testrun-confirmation',
        'addTestCasesToTestRun': 'App/TestRun/add-testcases-to-testrun',
        'assignTestCaseToUser': 'App/TestRun/assign-testcase-to-user',
        'runTestRun': 'App/TestRunResult/run-testrun',
        'testRunResultDetail': 'App/TestRunResult/testrun-result-detail',
        'testCaseDetail': 'App/TestCases/view-detail',
        'handlebars': 'Lib/handlebars.min',
        'highCharts': 'Lib/highcharts',
        'templateHelper': 'App/template-helper',
        'handlebarsHelper': 'App/handlebars-helper',
        'backbone': 'Lib/backbone-min',
        'underscore': 'Lib/underscore-min',
        'moment': "Lib/moment.min",
        'flight': 'Lib/flight.min',
        'bootstrap': 'Lib/bootstrap.min',
        'promise': 'Lib/bluebird.core.min',
        'simplebar': 'Lib/simplebar',
        'app': 'App/app'
    },
    'shim': {
        moment: {
            exports: 'moment'
        },
        flight: {
            exports: 'flight'
        },
        underscore: {
            exports: '_'
        },
        tinyMCE: {
            exports: 'tinyMCE',
            init: function () {
                this.tinyMCE.DOM.events.domLoaded = true;
                return this.tinyMCE;
            }
        },
        handlebars: {
            exports: 'Handlebars'
        }
    }
});
