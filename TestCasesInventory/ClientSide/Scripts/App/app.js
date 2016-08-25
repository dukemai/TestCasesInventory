define(['tinyMCEInit'], function () {

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

    return app;
});