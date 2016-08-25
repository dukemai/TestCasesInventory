
define(['handlebars'], function (handlebars) {

    var exportModule = {};
    exportModule.init = function (el) {


    };

    exportModule.templates = {};

    exportModule.displayTemplate = function (el, tmpl, data) {
        if (exportModule.templates[tmpl] === undefined) {
            return;
        }

        var template = exportModule.templates[tmpl];
        var html = template(data);
        el.html(html);
    }

    exportModule.loadAndCache = function (name, url) {
        if (exportModule.templates[name] === undefined) {
            $.get(url, function (data) {
                exportModule.templates[name] = handlebars.compile(data);;
            });
        }
    }

    return exportModule;
});