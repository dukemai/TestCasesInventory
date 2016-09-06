define(['tinyMCEInit', 'tabCommonFunctions', 'fileControl', 'addTestCasesToTestRun', 'deleteTestCasesInTestRunConfirmation',
    'assignTestCaseToUser', 'runTestRun'], function () {

    var app = {};
    app.init = function () {
        initEditors();
        initTabFunctions();
        initFileControl();
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

    function initFileControl() {
        var fileControl = require('fileControl');
        fileControl.init();
    }
   
    return app;
});