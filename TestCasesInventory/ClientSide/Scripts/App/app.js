define(['tinyMCEInit', 'tabCommonFunctions'], function () {

    var app = function () {
    };
    app.init = function () {
        initEditors();
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

    return app;
});