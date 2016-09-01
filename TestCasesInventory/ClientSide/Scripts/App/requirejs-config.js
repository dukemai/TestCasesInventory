requirejs.config({
    'baseUrl': '/ClientSide/Scripts',
    'paths': {
        'tinyMCE': 'Lib/tinymce/tinymce.min',
        'tinyMCEInit': 'App/tinymce-init',
        'tabCommonFunctions': 'App/tab-common-functions',
        'fileControl': 'App/file-control-custom',
        'deleteTestCasesInTestRunConfirmation': 'App/TestRun/delete-testcase-in-testrun-confirmation',
        'addTestCasesToTestRun': 'App/TestRun/add-testcases-to-testrun',
        'handlebars': 'Lib/handlebars.min',
        'templateHelper': 'App/template-helper',
        'backbone': 'Lib/backbone-min',
        'underscore': 'Lib/underscore-min',        
        'bootstrap': 'Lib/bootstrap.min',
        'promise': 'Lib/bluebird.core.min',
        'app': 'App/app'
    },
    'shim': {
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
