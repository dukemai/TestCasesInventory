define(['tinyMCEInit', 'tabCommonFunctions', 'fileDeleteConfirmation', 'addTestCasesToTestRun', 'deleteTestCasesInTestRunConfirmation',
    'assignTestCaseToUser', 'runTestRun', 'testRunResultByStatusShowChart', 'testRunResultByTesterShowChart'], function () {

    var app = {};
    app.init = function () {
        initEditors();
        initTabFunctions();
        initFileDeleteConfirmation();
    };
  
    app.initAddTestCasesToTestRun = function () {
        var addTestCasesToTestRun = require('addTestCasesToTestRun');
        addTestCasesToTestRun.init();
    };
    
    app.initDeleteTestCasesInTestRunConfirmation = function () {
        var deleteTestCasesInTestRunConfirmation = require('deleteTestCasesInTestRunConfirmation');
        deleteTestCasesInTestRunConfirmation.init();
    };

    app.initAssignTestCaseToUser = function () {
        var assignTestCaseToUser = require('assignTestCaseToUser');
        assignTestCaseToUser.init();
    }

    app.initRunTestRun = function () {
        var runTestRun = require('runTestRun');
        runTestRun.init();
    }

    app.initTestRunResultByTesterChart = function () {
        var testRunResultByTesterShowChart = require('testRunResultByTesterShowChart');
        testRunResultByTesterShowChart.init();
    };
    app.initTestRunResultByStatusChart = function () {
        var testRunResultByStatusShowChart = require('testRunResultByStatusShowChart');
        testRunResultByStatusShowChart.init();
    };

    function initEditors() {
        $('.tinymce-editor').each(function (index) {
            var tinyMCEInit = require('tinyMCEInit');
            tinyMCEInit.init(this);
        });
    }

    function initTabFunctions() {
        var tabCommonFunctions = require('tabCommonFunctions');
        tabCommonFunctions.init();
    }

    function initFileDeleteConfirmation() {
        var fileDeleteConfirmation = require('fileDeleteConfirmation');
        fileDeleteConfirmation.init();
    }
   
    return app;
});