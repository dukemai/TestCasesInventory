define(['tinyMCEInit', 'tabCommonFunctions', 'fileDeleteConfirmation', 'addTestCasesToTestRun', 'deleteTestCasesInTestRunConfirmation',
    'assignTestCaseToUser', 'runTestRun', 'testRunResultDetail', 'testCaseDetail', 'handlebarsHelper'], function () {

    var app = {};
    app.init = function () {
        initHandlebarsHelper();
        initEditors();
        initTabFunctions();
        initFileDeleteConfirmation();
    };

    app.initAddTestCasesToTestRun = function () {
        var addTestCasesToTestRun = require('addTestCasesToTestRun');
        addTestCasesToTestRun.init();
    }
    
    app.initDeleteTestCasesInTestRunConfirmation = function () {
        var deleteTestCasesInTestRunConfirmation = require('deleteTestCasesInTestRunConfirmation');
        deleteTestCasesInTestRunConfirmation.init();
    }

    app.initAssignTestCaseToUser = function () {
        var assignTestCaseToUser = require('assignTestCaseToUser');
        assignTestCaseToUser.init();
    }

    app.initRunTestRun = function () {
        var runTestRun = require('runTestRun');
        runTestRun.init();
    }

    app.initTestRunResultDetail = function () {
        var testRunResultDetail = require('testRunResultDetail');
        testRunResultDetail.init();
    }

    app.initShowTestCaseDetail = function () {
        var showTestCaseDetail = require('testCaseDetail');
        showTestCaseDetail.init();
    }

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
   
    function initHandlebarsHelper() {
        var handlebarsHelper = require('handlebarsHelper');
        handlebarsHelper.init();
    }

    return app;
});