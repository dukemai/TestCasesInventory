requirejs.config({
    'baseUrl': '/ClientSide/Scripts',
    'paths': {
        'tinyMCE': 'Lib/tinymce/tinymce.min',
        'tinyMCEInit': 'App/tinymce-init',
        'tabCommonFunctions': 'App/tab-common-functions',
        'fileControl': 'App/file-control-custom',
        'addTestCasesToTestRun': 'App/TestRun/add-testcases-to-testrun',
        'handlebars': 'Lib/handlebars.min',
        'templateHelper': 'App/template-helper',
        'backbone': 'Lib/backbone-min',
        'underscore': 'Lib/underscore-min',
        'jquery': 'Lib/jquery-1.9.1',
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
