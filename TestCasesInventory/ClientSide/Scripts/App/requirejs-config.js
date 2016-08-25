requirejs.config({
    'baseUrl': '/ClientSide/Scripts',
    'paths': {
        'tinyMCE': 'Lib/tinymce/tinymce.min',
        'tinyMCEInit': 'App/tinymce-init',
        'app':'App/app'
    },
    'shim': {
        'underscore': {
            'exports': '_'
        },
        tinyMCE: {
            exports: 'tinyMCE',
            init: function () {
                this.tinyMCE.DOM.events.domLoaded = true;
                return this.tinyMCE;
            }
        }
    }
});
