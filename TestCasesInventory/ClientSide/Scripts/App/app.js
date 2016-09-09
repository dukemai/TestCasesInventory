define(['tinyMCEInit', 'tabCommonFunctions', 'fileDeleteConfirmation', 'addTestCasesToTestRun', 'deleteTestCasesInTestRunConfirmation',
    'assignTestCaseToUser', 'runTestRun', 'testRunResultDetail'], function () {

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

    app.initTestRunResultDetail = function () {
        var testRunResultDetail = require('testRunResultDetail');
        testRunResultDetail.init();
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
   
    return app;
});